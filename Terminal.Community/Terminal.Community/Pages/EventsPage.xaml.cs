using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class EventsPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public EventsPage()
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.EventsPageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var model = BindingContext as Models.Page.EventsPageModel;

            if (!IsEnabled)
            {
                model.IsBusy = true;

                var stopwatch = new System.Diagnostics.Stopwatch();

                System.Threading.Tasks.Task.Run(() =>
                {
                    stopwatch.Start();

                    var list = new System.Collections.Generic.List<Models.View.DayEventsViewModel>();

                    using (var dbContext = Providers.DdContextProvider.GetDbContext())
                    {
                        var events = dbContext.Events
                                .Include(entity => entity.PosterImage)
                                .Include(entity => entity.EventPersonalityBridges).ThenInclude(bridge => bridge.Personality).ThenInclude(entity => entity.Image)
                                .OrderBy(entity => entity.Start).ToArray();

                        Models.View.DayEventsViewModel currentDayEventsViewModel = null;

                        foreach (var @event in events)
                        {
                            if (currentDayEventsViewModel == null || currentDayEventsViewModel.Date.Day != @event.Start.Date.Day)
                            {
                                if (currentDayEventsViewModel != null)
                                {
                                    list.Add(currentDayEventsViewModel);
                                }

                                currentDayEventsViewModel = new Models.View.DayEventsViewModel { Date = @event.Start.Date };
                            }

                            currentDayEventsViewModel.Add(new Models.View.EventViewModel
                            {
                                Id = @event.Id,
                                Header = @event.Header,
                                Title = @event.Title,
                                Start = @event.Start,
                                End = @event.End,
                                Organizer = @event.Organizer,
                                Place = @event.Place,
                                Thematic = @event.Thematic,
                                Presenters = @event.EventPersonalityBridges.Where(e => e.IsPresenter).Select(p => new Models.View.PersonalityViewModel
                                {
                                    Id = p.Personality.Id,
                                    BornDate = p.Personality.BornDate,
                                    Name = p.Personality.Name,
                                    LastName = p.Personality.LastName,
                                    IsMale = p.Personality.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male,
                                    About = p.Personality.About,
                                    ImageId = p.Personality.Image?.Id ?? System.Guid.Empty
                                }).ToArray(),
                                Guests = @event.EventPersonalityBridges.Where(e => !e.IsPresenter).Select(p => new Models.View.PersonalityViewModel
                                {
                                    Id = p.Personality.Id,
                                    BornDate = p.Personality.BornDate,
                                    Name = p.Personality.Name,
                                    LastName = p.Personality.LastName,
                                    IsMale = p.Personality.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male,
                                    About = p.Personality.About,
                                    ImageId = p.Personality.Image?.Id ?? System.Guid.Empty
                                }).ToArray(),
                                About = @event.About,
                                Description = @event.Description,
                                PosterImageId = @event.PosterImage.Id
                            });
                        }

                        if (currentDayEventsViewModel != null)
                        {
                            list.Add(currentDayEventsViewModel);
                        }
                    }

                    return list;
                }).ContinueWith(async task =>
                {
                    stopwatch.Stop();

                    var reasonableElapseTime = System.TimeSpan.FromSeconds(1);

                    if (stopwatch.Elapsed < reasonableElapseTime)
                    {
                        await System.Threading.Tasks.Task.Delay(reasonableElapseTime - stopwatch.Elapsed);
                    }

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        if (task.Exception == null)
                        {
                            model.DayEvents = new System.Collections.ObjectModel.ObservableCollection<Models.View.DayEventsViewModel>(task.Result);
                        }
                        else
                        {
                            System.Exception targetException = task.Exception;

                            while (targetException is System.AggregateException)
                            {
                                targetException = targetException.InnerException;
                            }

                            if (targetException is System.TypeLoadException)
                            {
                                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("Aviso", string.Format("Con la actualización de la aplicación, actualizar la base de datos es necesario para continuar.{0}{0}Disculpe las molestias que esto pueda ocasionar.", System.Environment.NewLine), "Aceptar", "Salir").ContinueWith((System.Threading.Tasks.Task<bool> alertTask) =>
                                    {
                                        if (alertTask.Result)
                                        {
                                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                                            {
                                                ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).PresentDatabaseStatusSection();
                                            });
                                        }
                                        else
                                        {
                                            Xamarin.Forms.DependencyService.Get<Managers.Facade.IApplicationManagerFacade>().Close();
                                        }
                                    });
                                });
                            }
                        }

                        model.IsBusy = false;

                        IsEnabled = true;
                    });
                });
            }
        }

        private void DatabaseUpdateButton_Clicked(object sender, System.EventArgs e)
        {
            ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).PresentDatabaseStatusSection();
        }

        private void EventOverviewFrame_Tapped(object sender, System.EventArgs e)
        {
            this.NavigationPushPageAsync(new EventDetailPage((sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.EventViewModel));
        }
    }
}