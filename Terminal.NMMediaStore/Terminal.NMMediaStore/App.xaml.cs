using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Constant;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore
{
    public partial class App : Xamarin.Forms.Application, Helpers.Facade.IExceptionHandlingHelperFacade
    {
        public App()
        {
            InitializeComponent();

            XFToolKit.IoC.TinyIoCContainer.Current.Register<Managers.Facade.ISubscriptionManagerFacade, Managers.SubscriptionManager>();
            XFToolKit.IoC.TinyIoCContainer.Current.Register<Managers.Facade.IFetchOrderManagerFacade, Managers.FetchOrderManager>();
            // TODO Replace Manager's mock implementation at IoC registration
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

            XFToolKit.IoC.TinyIoCContainer.Current.Register<Helpers.Facade.IExceptionHandlingHelperFacade>(this);

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

        public void HandleException(System.Exception exception, System.Action callbackAction = null)
        {
            if (exception == null)
            {
                MainPage.DisplayAlert(StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, StringConstantProvider.TASK_CANCELED_EXCEPTION_MESSAGE, StringConstantProvider.OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(); });

                return;
            }
            
            if (exception.InnerException is Novell.Directory.Ldap.LdapException)
            {
                var concreteException = exception.InnerException as Novell.Directory.Ldap.LdapException;

                switch (concreteException.ResultCode)
                {
                    case Novell.Directory.Ldap.LdapException.InvalidCredentials:
                        MainPage.DisplayAlert(StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, StringConstantProvider.INVALID_CREDENTIALS_LDAP_EXCEPTION_MESSAGE, StringConstantProvider.OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(); });

                        break;

                    default:
                        MainPage.DisplayAlert(StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, StringConstantProvider.UNSPECTED_LDAP_EXCEPTION_MESSAGE, StringConstantProvider.OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(); });

                        break;
                }
            }
            else if (exception.InnerException is Common.Exceptions.UnspectedHttpStatusException)
            {
                var concreteException = exception.InnerException as Common.Exceptions.UnspectedHttpStatusException;

                if (concreteException.HttpStatusCode == System.Net.HttpStatusCode.UpgradeRequired)
                {
                    MainPage.DisplayAlert(StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, StringConstantProvider.UPGRADE_REQUIRED_EXCEPTION_MESSAGE, StringConstantProvider.OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(); });
                }
                else
                {
                    MainPage.DisplayAlert(StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, StringConstantProvider.UNSPECTED_API_RESPONSE_EXCEPTION_MESSAGE, StringConstantProvider.OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(); });
                }
            }
            else if (exception.InnerException is System.Net.Http.HttpRequestException || exception.InnerException.InnerException is System.Net.Sockets.SocketException)
            {
                MainPage.DisplayAlert(StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, StringConstantProvider.NETWORK_EXCEPTION_MESSAGE, StringConstantProvider.OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(); });
            }
            else
            {
                MainPage.DisplayAlert(StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, StringConstantProvider.UNSPECTED_EXCEPTION_MESSAGE, StringConstantProvider.OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(); });
            }
        }
    }
}
