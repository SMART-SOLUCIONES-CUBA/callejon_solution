using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    [System.ComponentModel.DesignTimeVisible(false)]
    public partial class CommentView : Xamarin.Forms.ContentView
    {
        private const string REVOKE_ACTION_NAME = "Retirar Denuncia";

        private const string DELATE_ACTION_NAME = "Denunciar";

        private const string RESPONSE_ACTION_NAME = "Responder";

        private readonly System.Action responseSuccessCompletitionAction;

        public CommentView(Models.View.AbstractCommentViewModel viewModel, System.Action responseSuccessCompletitionAction)
        {
            this.responseSuccessCompletitionAction = responseSuccessCompletitionAction;

            InitializeComponent();

            BindingContext = viewModel;

            if (viewModel.Reference != null)
            {
                ReferenceContainerGrid.IsVisible = true;

                ReferenceClientNameLabel.Text = viewModel.Reference.ClientName;
                ReferenceMessageLabel.Text = viewModel.Reference.Message;
            }

            if (viewModel.LastDelation != null)
            {
                DelationIdicatorLabel.IsVisible = true;
            }
        }

        public void OptionButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var actions = new string[] { RESPONSE_ACTION_NAME, DELATE_ACTION_NAME };

            if ((BindingContext as Models.View.AbstractCommentViewModel).LastDelation != null)
            {
                actions = new string[] { RESPONSE_ACTION_NAME, REVOKE_ACTION_NAME };
            }

            Xamarin.Forms.Application.Current.MainPage.DisplayActionSheet("Opciones de Comentario", "Cancelar", null, actions).ContinueWith((System.Threading.Tasks.Task<string> task) =>
            {
                if (task.Result == RESPONSE_ACTION_NAME)
                {
                    if (BindingContext is Models.View.EventCommentViewModel)
                    {
                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new Pages.Popup.CommentEventPopupPage((BindingContext as Models.View.EventCommentViewModel).Event, responseSuccessCompletitionAction, BindingContext as Models.View.AbstractCommentViewModel));

                        return;
                    }

                    if (BindingContext is Models.View.PersonalityCommentViewModel)
                    {
                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new Pages.Popup.CommentPersonalityPopupPage((BindingContext as Models.View.PersonalityCommentViewModel).Personality, responseSuccessCompletitionAction, BindingContext as Models.View.AbstractCommentViewModel));

                        return;
                    }

                    if (BindingContext is Models.View.ProductionCommentViewModel)
                    {
                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new Pages.Popup.CommentProductionPopupPage((BindingContext as Models.View.ProductionCommentViewModel).Production, responseSuccessCompletitionAction, BindingContext as Models.View.AbstractCommentViewModel));
                    }
                }
                else if (task.Result == DELATE_ACTION_NAME)
                {
                    Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new Pages.Popup.DelateCommentPopupPage(BindingContext as Models.View.AbstractCommentViewModel, responseSuccessCompletitionAction));
                }
                else if (task.Result == REVOKE_ACTION_NAME)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Confirmar", "Está seguro que desea retirar la denuncia a este comentario?", "Retirar", "Cancelar").ContinueWith((System.Threading.Tasks.Task<bool> confirmationTask) =>
                        {
                            if (!confirmationTask.Result) return;

                            var commentViewModel = BindingContext as Models.View.AbstractCommentViewModel;

                            using (var dbContext = Providers.DdContextProvider.GetDbContext())
                            {
                                if (commentViewModel is Models.View.EventCommentViewModel)
                                {
                                    var delation = dbContext.EventCommentDelations.First(entity => entity.Id == commentViewModel.LastDelation.Id);

                                    delation.Revoked = true;

                                    dbContext.EventCommentDelations.Update(delation);
                                }
                                else if (commentViewModel is Models.View.PersonalityCommentViewModel)
                                {
                                    var delation = dbContext.PersonalityCommentDelations.First(entity => entity.Id == commentViewModel.LastDelation.Id);

                                    delation.Revoked = true;

                                    dbContext.PersonalityCommentDelations.Update(delation);
                                }
                                else if (commentViewModel is Models.View.ProductionCommentViewModel)
                                {
                                    var delation = dbContext.ProductionCommentDelations.First(entity => entity.Id == commentViewModel.LastDelation.Id);

                                    delation.Revoked = true;

                                    dbContext.ProductionCommentDelations.Update(delation);
                                }

                                dbContext.SaveChanges("client");
                            }

                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso", "Cambios aplicados con éxito.", "Aceptar");

                                responseSuccessCompletitionAction?.Invoke();
                            });
                        });
                    });
                }
            });
        }
    }
}