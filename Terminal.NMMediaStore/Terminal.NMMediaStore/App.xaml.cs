namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            XFToolKit.IoC.TinyIoCContainer.Current.Register<Managers.Facade.ISubscriptionManagerFacade, Managers.MockSubscriptionManager>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Managers.Facade.IFetchOrderManagerFacade, Managers.MockFetchOrderManager>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Managers.Facade.ICommentManagerFacade, Managers.MockCommentManager>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Managers.Facade.IProductionCheckoutManagerFacade, Managers.MockProductionCheckoutManager>();

            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IImageSourceProviderFacade, Providers.ImageSourceProvider>();
            // TODO Replace Provider's mock implementation at IoC registration
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.ICategoryProviderFacade, Providers.MockCategoryProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IProducerHouseProviderFacade, Providers.MockProducerHouseProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IGenreProviderFacade, Providers.MockGenreProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IYearProviderFacade, Providers.MockYearProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IPersonalityProviderFacade, Providers.MockPersonalityProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.ICountryProviderFacade, Providers.MockCountryProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IAdvertisementProviderFacade, Providers.MockAdvertisementProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IAudioVisualProviderFacade, Providers.MockAudioVisualProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.ITagProviderFacade, Providers.MockTagProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.ISearchProviderFacade, Providers.MockSearchProvider>();

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
