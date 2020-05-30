using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DatabaseUpdatePage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private readonly bool forceUpgrade;

        public DatabaseUpdatePage(bool forceUpgrade)
        {
            InitializeComponent();

            this.forceUpgrade = forceUpgrade;

            BindingContext = new XFToolKit.Mvvm.Models.Page.BasePageModel
            {
                IsBusy = true,
                BusyProgressMessage = "Iniciando ..."
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Helpers.DataSyncronizationHelper.Instance.SyncronizeDataFromCloud(UpdateDatabaseProgressCallback, UpdateDatabaseTaskCompleteCallback, forceUpgrade);
        }

        private void UpdateDatabaseProgressCallback(int progress, bool isBusy, string step)
        {
            (BindingContext as XFToolKit.Mvvm.Models.Page.BasePageModel).IsBusy = isBusy;
            (BindingContext as XFToolKit.Mvvm.Models.Page.BasePageModel).BusyProgressMessage = step;
        }

        private void UpdateDatabaseTaskCompleteCallback(System.Threading.Tasks.Task<bool> task)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                if (task.Exception != null)
                {
                    UpdateDatabaseProgressCallback(0, false, "Actualización fallida.");

                    System.Exception targetException = task.Exception;

                    while (targetException is System.AggregateException)
                    {
                        targetException = targetException.InnerException;
                    }

                    var message = "Error 101. Interrupción no esperada. Contacte con el servicio de soporte para recibir asistecia.";

                    if (targetException is System.Net.Http.HttpRequestException)
                    {
                        if (targetException.Message.Contains(((int)System.Net.HttpStatusCode.UpgradeRequired).ToString()))
                        {
                            message = "Es necesario actualizar la aplicación para poder continuar.";
                        }
                        else
                        {
                            message = "Error al establecer la comunicación con el servicio. Verifique la configuración de red de sus dispositivo.";
                        }
                    }
                    else if (targetException is System.Threading.Tasks.TaskCanceledException)
                    {
                        message = "La comunicación con el servicio ha tomado demasiado tiempo. Verifique la configuración de red de su dispositivo.";
                    }

                    DisplayAlert("Aviso", message, "Aceptar");
                }
                else
                {
                    UpdateDatabaseProgressCallback(0, false, "Actualización exitosa.");

                    Xamarin.Forms.Application.Current.Properties[Providers.ConstantsProvider.API_KEY_SECRET_PROPERTY_KEY] = Providers.ConstantsProvider.API_KEY_SECRET;

                    Xamarin.Forms.Application.Current.SavePropertiesAsync();

                    DisplayAlert("Aviso", "Datos actualizados con éxito.", "Aceptar");
                }

                this.NavigationPopModalPageAsync();
            });
        }
    }
}