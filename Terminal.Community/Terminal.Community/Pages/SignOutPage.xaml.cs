using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class SignOutPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public SignOutPage()
        {
            InitializeComponent();

            BindingContext = new XFToolKit.Mvvm.Models.Page.BasePageModel();
        }

        private void SignOutButton_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Confirmar", "Esta seguro que desea cerrar sesión y con ello eliminar todos los datos en la aplicación?", "Cerrar Sesión", "Cancelar").ContinueWith(alertTask =>
            {
                if (!alertTask.Result) return;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as XFToolKit.Mvvm.Models.Page.BasePageModel).IsBusy = true;

                    System.Threading.Tasks.Task.Run(async () =>
                    {
                        await Managers.ProfileManager.Instance.ClearCurrentProfileAsync();

                        Helpers.LocalFileStorageHelper.DeleteLocalFiles();
                    }).ContinueWith(task =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            (((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).BindingContext as Models.Page.MasterPageModel).Profile = null;

                            ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).PresentHomeSection(true);

                            (Xamarin.Forms.Application.Current.MainPage as MainPage).IsEnabled = false;

                            (BindingContext as XFToolKit.Mvvm.Models.Page.BasePageModel).IsBusy = false;

                            DisplayAlert("Aviso", "Sesión cerrada con éxito.", "Aceptar").ContinueWith(displayAlertTask =>
                            {
                                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                                {
                                    Xamarin.Forms.Application.Current.MainPage.NavigationPushModalPageAsync(new SessionConfigurationPage());
                                });
                            });
                        });
                    });
                });
            });
        }
    }
}