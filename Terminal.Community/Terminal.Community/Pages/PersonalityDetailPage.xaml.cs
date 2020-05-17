using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class PersonalityDetailPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public PersonalityDetailPage(Models.View.PersonalityViewModel model)
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = model;
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
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new Popup.CommentPersonalityPopupPage(BindingContext as Models.View.PersonalityViewModel, UpdateComments));
        }

        private void UpdateComments()
        {
            var model = BindingContext as Models.View.PersonalityViewModel;

            System.Threading.Tasks.Task.Run(() =>
            {
                var comments = new System.Collections.Generic.List<SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.PersonalityComment>();

                using (var dbContext = Providers.DdContextProvider.GetDbContext())
                {
                    comments.AddRange(dbContext.PersonalityComments.Where(entity => entity.Personality.Id == model.Id)
                        .Include(entity => entity.Delations)
                        .Include(entity => entity.Client)
                        .ThenInclude(entity => entity.Image)
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
                            var commentViewModel = new Models.View.PersonalityCommentViewModel
                            {
                                Id = comment.Id,
                                ClientName = string.Format("{0} {1}", comment.Client.FirstName.Trim(), comment.Client.LastName.Trim()),
                                ClientImageSource = comment.Client.Image != null ? Providers.ImageSourceProvider.Instance.Cached(comment.Client.Image.Id) : (comment.Client.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male ? Providers.ImageSourceProvider.Instance.ProfileMaleDefault() : Providers.ImageSourceProvider.Instance.ProfileFemaleDefault()),
                                Created = comment.Created,
                                Message = comment.Message,
                                Personality = model
                            };

                            if (comment.Reference != null)
                            {
                                commentViewModel.Reference = new Models.View.PersonalityCommentViewModel
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
                        CommentsContainer.Children.Add(new Xamarin.Forms.Label { Text = "Sin elementos que mostrar.", TextColor = Xamarin.Forms.Color.DimGray, FontSize = Helpers.SizeProvisioningHelper.Instance.EventLittleEmphasisStyleFontSize });
                    }
                });
            });
        }
    }
}