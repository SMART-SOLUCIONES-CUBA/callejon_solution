namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            // TODO Replace Manager's mock implementation at IoC registration
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Managers.Facade.ISubscriptionManagerFacade, Managers.MockSubscriptionManager>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Managers.Facade.IFetchOrderManagerFacade, Managers.MockFetchOrderManager>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Managers.Facade.ICommentManagerFacade, Managers.MockCommentManager>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Managers.Facade.IProductionCheckoutManagerFacade, Managers.MockProductionCheckoutManager>();

            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IImageSourceProviderFacade, Providers.ImageSourceProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IGenreProviderFacade, Providers.RemoteGenreProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.ICategoryProviderFacade, Providers.RemoteCategoryProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.ITagProviderFacade, Providers.RemoteTagProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.ICountryProviderFacade, Providers.RemoteCountryProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.ISearchProviderFacade, Providers.RemoteSearchProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IYearProviderFacade, Providers.RemoteYearProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IProducerHouseProviderFacade, Providers.RemoteProducerHouseProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IPersonalityProviderFacade, Providers.RemotePersonalityProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IAdvertisementProviderFacade, Providers.RemoteAdvertisementProvider>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Providers.Facade.IAudioVisualProviderFacade, Providers.RemoteAudioVisualProvider>();            

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
