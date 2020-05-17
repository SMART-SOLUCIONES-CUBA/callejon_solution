using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages.Popup
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class CommentPersonalityPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private Models.View.PersonalityViewModel personalityViewModel;
        private System.Action successCompletitionAction;
        private Models.View.AbstractCommentViewModel commentViewModel;

        public CommentPersonalityPopupPage(Models.View.PersonalityViewModel personalityViewModel, System.Action successCompletitionAction = null, Models.View.AbstractCommentViewModel commentViewModel = null)
        {
            this.personalityViewModel = personalityViewModel;
            this.successCompletitionAction = successCompletitionAction;
            this.commentViewModel = commentViewModel;

            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;

            CommentEditor.HeightRequest = Helpers.SizeProvisioningHelper.Instance.EventDefaultStyleFontSize * 10;

            if (commentViewModel != null)
            {
                HeaderLabel.Text = "Responder Comentario de Personalidad";
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return !string.IsNullOrEmpty(CommentEditor.Text);
        }

        private void PopupPage_BackgroundClicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(CommentEditor.Text))
            {
                Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }
        }

        private void CancelButton_Clicked(object sender, System.EventArgs e)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        private void ApplyButton_Clicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(CommentEditor.Text) || WorkingIndicationContainer.IsVisible) return;

            (sender as Xamarin.Forms.Button).IsEnabled = false;

            WorkingIndicationContainer.IsVisible = true;

            System.Threading.Tasks.Task.Run(() =>
            {
                using (var dbContext = Providers.DdContextProvider.GetDbContext())
                {
                    var client = dbContext.Clients.FirstOrDefault(entity => entity.Id == Managers.ProfileManager.Instance.GetCurrentProfile().Id);

                    if (client == null)
                    {
                        var profile = Managers.ProfileManager.Instance.GetCurrentProfile();

                        client = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.Client
                        {
                            Id = profile.Id,
                            FirstName = profile.FirstName,
                            LastName = profile.LastName,
                            PhoneNumber = profile.Phone,
                            Sex = profile.IsMale ? SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male : SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Famale
                        };
                    }

                    var comment = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.PersonalityComment
                    {
                        Client = client,
                        Personality = dbContext.Personalities.First(entity => entity.Id == personalityViewModel.Id),
                        Message = CommentEditor.Text
                    };

                    if (commentViewModel != null)
                    {
                        comment.Reference = dbContext.PersonalityComments.First(entity => entity.Id == commentViewModel.Id);
                    }

                    dbContext.PersonalityComments.Add(comment);

                    dbContext.SaveChanges("client");
                }
            }).ContinueWith((System.Threading.Tasks.Task task) =>
            {
                var exit = true;

                var message = "Cambios aplicados con éxito.";

                if (task.Exception != null)
                {
                    message = "No fue posible aplicar los cambios en este momento.";

                    exit = false;
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Aviso", message, "Aceptar");

                    WorkingIndicationContainer.IsVisible = false;

                    if (exit)
                    {
                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();

                        successCompletitionAction?.Invoke();
                    }
                });
            });
        }
    }
}