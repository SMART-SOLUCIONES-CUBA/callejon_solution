namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [System.ComponentModel.DesignTimeVisible(false)]
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            
            if (IsEnabled) return;

            var profile = Managers.ProfileManager.Instance.GetCurrentProfile();

            if (profile == null)
            {
                Navigation.PushModalAsync(new SessionConfigurationPage());
            }
            else
            {
                (Master.BindingContext as Models.Page.MasterPageModel).Profile = profile;

                IsEnabled = true;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if ((Detail as Xamarin.Forms.NavigationPage).CurrentPage == (Detail as Xamarin.Forms.NavigationPage).RootPage)
            {
                if ((Detail as Xamarin.Forms.NavigationPage).CurrentPage is HomePage)
                {
                    DisplayAlert("Aviso", "Está seguro que desea salir de la aplicación?", "Salir", "Cancelar").ContinueWith((System.Threading.Tasks.Task<bool> task) =>
                    {
                        if (task.Result)
                        {
                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                Xamarin.Forms.DependencyService.Get<Managers.Facade.IApplicationManagerFacade>().Close();
                            });
                        }
                    });
                }
                else
                {
                    (Master as MasterPage).PresentHomeSection();
                }

                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}
