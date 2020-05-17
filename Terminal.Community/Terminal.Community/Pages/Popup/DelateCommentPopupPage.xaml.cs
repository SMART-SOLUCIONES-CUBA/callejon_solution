using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages.Popup
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DelateCommentPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private Models.View.AbstractCommentViewModel commentViewModel;
        private System.Action successCompletitionAction;

        public DelateCommentPopupPage(Models.View.AbstractCommentViewModel commentViewModel, System.Action successCompletitionAction = null)
        {
            this.commentViewModel = commentViewModel;
            this.successCompletitionAction = successCompletitionAction;

            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;

            CommentEditor.HeightRequest = Helpers.SizeProvisioningHelper.Instance.EventDefaultStyleFontSize * 10;
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
            if (string.IsNullOrEmpty(CommentEditor.Text)) return;

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

                    if (commentViewModel is Models.View.EventCommentViewModel)
                    {
                        var delation = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.EventCommentDelation
                        {
                            Id = System.Guid.NewGuid(),
                            Created = System.DateTime.Now,
                            Note = CommentEditor.Text,
                            Client = client,
                            Comment = dbContext.EventComments.First(entity => entity.Id == commentViewModel.Id)
                        };

                        dbContext.EventCommentDelations.Add(delation);
                    }
                    else if (commentViewModel is Models.View.PersonalityCommentViewModel)
                    {
                        var delation = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.PersonalityCommentDelation
                        {
                            Id = System.Guid.NewGuid(),
                            Created = System.DateTime.Now,
                            Note = CommentEditor.Text,
                            Client = client,
                            Comment = dbContext.PersonalityComments.First(entity => entity.Id == commentViewModel.Id)
                        };

                        dbContext.PersonalityCommentDelations.Add(delation);
                    }
                    else if (commentViewModel is Models.View.ProductionCommentViewModel)
                    {
                        var delation = new SAPRESSC.Business.Billboard.CPCC.Persistence.Entities.ProductionCommentDelation
                        {
                            Id = System.Guid.NewGuid(),
                            Created = System.DateTime.Now,
                            Note = CommentEditor.Text,
                            Client = client,
                            Comment = dbContext.ProductionComments.First(entity => entity.Id == commentViewModel.Id)
                        };

                        dbContext.ProductionCommentDelations.Add(delation);
                    }

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