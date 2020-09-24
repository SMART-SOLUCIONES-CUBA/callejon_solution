using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Constant;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class SignInPage : Xamarin.Forms.ContentPage
    {
        private System.Action callbackAction;

        public SignInPage(System.Action callbackAction)
        {
            InitializeComponent();

            this.callbackAction = callbackAction;

            BindingContext = new Models.Page.SignInPageModel();
        }

        private Models.Page.SignInPageModel BindingContextModel
        {
            get
            {
                return BindingContext as Models.Page.SignInPageModel;
            }
        }

        private void AuthenticateButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (string.IsNullOrEmpty(BindingContextModel.SubscriptorId) || string.IsNullOrEmpty(BindingContextModel.Password))
            {
                DisplayAlert("Aviso", "Ambos campos son requeridos para poder identificarlo como usuario.", "Aceptar");

                return;
            }

            BindingContextModel.IsBusy = true;

            var subscriptionManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>();

            subscriptionManager.AuthenticateAsync(BindingContextModel.SubscriptorId, BindingContextModel.Password).ContinueWith(authenticationTask =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (authenticationTask.Exception == null)
                    {
                        Navigation.PopModalAsync();

                        callbackAction?.Invoke();

                        callbackAction = null;
                    }
                    else
                    {
                        XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(authenticationTask.Exception);
                    }

                    BindingContextModel.IsBusy = false;
                });
            });
        }
    }
}