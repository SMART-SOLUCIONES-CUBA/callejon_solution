using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class EventDetailPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public EventDetailPage(Models.View.EventViewModel model)
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext =  model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!IsEnabled)
            {
                IsEnabled = true;

                UpdateComments();
            }
        }

        private void CommentToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new Popup.CommentEventPopupPage(BindingContext as Models.View.EventViewModel, UpdateComments));
        }

        private void SheduleEventToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
        {
            Xamarin.Essentials.Permissions.CheckStatusAsync<Plugins.Essentials.Permission.CalendarReadAndWritePermission>().ContinueWith(checkPermissionStatusTask =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (checkPermissionStatusTask.Result != Xamarin.Essentials.PermissionStatus.Granted)
                    {
                        Xamarin.Essentials.Permissions.RequestAsync<Plugins.Essentials.Permission.CalendarReadAndWritePermission>().ContinueWith(permissionRequestTask =>
                        {
                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                if (permissionRequestTask.Result == Xamarin.Essentials.PermissionStatus.Granted)
                                {
                                    DisplayAlert("Confirmar", "Esta seguro que desea agregar al calendario este evento?", "Agregar", "Cancelar")
                                    .ContinueWith(async task =>
                                    {
                                        if (!task.Result) return;

                                        await SchuddleEventAsync();
                                    });
                                }
                                else
                                {
                                    DisplayAlert("Permiso Denegado", "No se puede continuar sin el permiso solicitado. Inténtelo nuevamente.", "Aceptar");
                                }
                            });                            
                        });
                    }
                    else
                    {
                        DisplayAlert("Confirmar", "Esta seguro que desea agregar al calendario este evento?", "Agregar", "Cancelar")
                        .ContinueWith(async task =>
                        {
                            if (!task.Result) return;

                            await SchuddleEventAsync();
                        });
                    }
                });
            });
        }

        private void PersonalityOverview_Tapped(object sender, System.EventArgs e)
        {
            var personality = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.PersonalityViewModel;

            Navigation.PushAsync(new PersonalityDetailPage(personality));
        }

        private async System.Threading.Tasks.Task SchuddleEventAsync()
        {
            try
            {
                var calendars = await Plugin.Calendars.CrossCalendars.Current.GetCalendarsAsync();

                var model = BindingContext as Models.View.EventViewModel;

                var calendarEvent = new Plugin.Calendars.Abstractions.CalendarEvent
                {
                    Name = string.Format("{0}: {1}", model.Header.ToUpper(), model.Title),
                    Start = model.Start,
                    End = model.End,
                    Location = model.Place,
                    Description = model.About,
                    Reminders = new System.Collections.Generic.List<Plugin.Calendars.Abstractions.CalendarEventReminder> { new Plugin.Calendars.Abstractions.CalendarEventReminder() }
                };

                await Plugin.Calendars.CrossCalendars.Current.AddOrUpdateEventAsync(calendars.First(), calendarEvent);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Aviso", "Evento agregado con éxito al calendario.", "Aceptar");
                });
            }
            catch
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Aviso", "No fue posible agregar el evento al calendario en este momento.", "Aceptar");
                });
            }
        }

        private void UpdateComments()
        {
            var model = BindingContext as Models.View.EventViewModel;

            System.Threading.Tasks.Task.Run(() =>
            {
                var comments = new System.Collections.Generic.List<SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.EventComment>();

                using (var dbContext = Providers.DdContextProvider.GetDbContext())
                {
                    comments.AddRange(dbContext.EventComments.Where(entity => entity.Event.Id == model.Id)
                        .Include(entity => entity.Delations)
                        .Include(entity => entity.Client).ThenInclude(entity => entity.Image)
                        .OrderBy(entity => entity.Created)
                        .ToArray());
                }

                return comments;
            }).ContinueWith(task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    CommentsContainer.Children.Clear();

                    if (task.Result.Count > 0)
                    {
                        foreach (var comment in task.Result)
                        {
                            var commentViewModel = new Models.View.EventCommentViewModel
                            {
                                Id = comment.Id,
                                ClientName = string.Format("{0} {1}", comment.Client.FirstName.Trim(), comment.Client.LastName.Trim()),
                                ClientImageSource = comment.Client.Image != null ? Providers.ImageSourceProvider.Instance.Cached(comment.Client.Image.Id) : (comment.Client.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male ? Providers.ImageSourceProvider.Instance.ProfileMaleDefault() : Providers.ImageSourceProvider.Instance.ProfileFemaleDefault()),
                                Created = comment.Created,
                                Message = comment.Message,
                                Event = model
                            };

                            if (comment.Reference != null)
                            {
                                commentViewModel.Reference = new Models.View.EventCommentViewModel
                                {
                                    Id = comment.Reference.Id,
                                    ClientName = string.Format("{0} {1}", comment.Reference.Client.FirstName.Trim(), comment.Reference.Client.LastName.Trim()),
                                    Created = comment.Reference.Created,
                                    Message = comment.Reference.Message
                                };
                            }

                            if (comment.Delations.Where(entity => !entity.Revoked).Any())
                            {
                                var lastDelation = comment.Delations.Where(entity => !entity.Revoked).OrderBy(entity => entity.Created).Last();

                                commentViewModel.LastDelation = new Models.View.CommentDelationViewModel
                                {
                                    Id = lastDelation.Id,
                                    Created = lastDelation.Created,
                                    Note = lastDelation.Note
                                };
                            }

                            var commentView = new Views.CommentView(commentViewModel, UpdateComments);

                            CommentsContainer.Children.Add(commentView);
                        }
                    }
                    else
                    {
                        CommentsContainer.Children.Add(new Xamarin.Forms.Label 
                        { 
                            Text = "Sin elementos que mostrar.", 
                            TextColor = Xamarin.Forms.Color.DimGray,
                            FontSize = Helpers.SizeProvisioningHelper.Instance.EventLittleEmphasisStyleFontSize 
                        });
                    }
                });
            });
        }
    }
}