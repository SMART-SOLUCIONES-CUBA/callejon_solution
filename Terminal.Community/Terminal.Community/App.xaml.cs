using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            FFImageLoading.Config.Configuration.Default.DiskCacheDuration = System.TimeSpan.MaxValue;

            MainPage = new Pages.MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
