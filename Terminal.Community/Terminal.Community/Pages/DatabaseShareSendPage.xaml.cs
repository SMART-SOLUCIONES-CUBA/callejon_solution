using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DatabaseShareSendPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public DatabaseShareSendPage()
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.DatabaseSharePageModel
            {
                IsBusy = true,
                BusyProgressMessage = "Configurando",
                BusyProgressMessageDetail = "Preparando punto de acceso WiFi para tus amigos ..."
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!IsEnabled)
            {
                IsEnabled = true;

                Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostPotManagerFacade>().RegisterCallbackActions(CheckPermissionsCallback, StartupRequestCallback, ShutdownRequestCallback);

                Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostPotManagerFacade>().CheckPermissions();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            OnClosing();

            return true;
        }

        private void CloseButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            OnClosing();
        }

        private void OnClosing()
        {
            DisplayAlert("Confirmar", "Está seguro que desea terminar el proceso Compartir con Amigos?", "Terminar", "Cancelar").ContinueWith(task =>
            {
                if (!task.Result) return;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    var pageModel = BindingContext as Models.Page.DatabaseSharePageModel;

                    pageModel.IsBusy = true;
                    pageModel.BusyProgressMessage = "Terminado";
                    pageModel.BusyProgressMessageDetail = "Apagando punto de acceso WiFi ...";

                    IsEnabled = false;

                    Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostPotManagerFacade>().RequestShutdown();

                    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(2));

                    await this.NavigationPopModalPageAsync();
                });                
            });
        }

        private void CheckPermissionsCallback(bool success)
        {
            if (success)
            {
                Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostPotManagerFacade>().RequestStartup();
            }
            else
            {
                DisplayAlert("Aviso", "Permisos requeridos", "Aceptar");

                this.NavigationPopModalPageAsync();
            }
        }

        private void StartupRequestCallback(Models.View.WiFiHostPotDetailViewModel model)
        {
            System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(2)).ContinueWith(task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var pageModel = BindingContext as Models.Page.DatabaseSharePageModel;

                    pageModel.WiFiHostPotDetail = model;
                    pageModel.IsBusy = false;
                });
            });            
        }

        private void ShutdownRequestCallback(bool success)
        {
            if (!IsEnabled) return;
        }
    }
}