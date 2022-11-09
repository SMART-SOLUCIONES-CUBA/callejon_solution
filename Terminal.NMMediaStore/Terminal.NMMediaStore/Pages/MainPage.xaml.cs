using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [System.ComponentModel.DesignTimeVisible(false)]
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

            var subscriptionManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>();

            var subscription = subscriptionManager.GetAuthenticatedSubscrition();

            if (subscription == null)
            {
                this.NavigationPushModalPageAsync(new SignInPage(OnAppearing));
            }
            else if (subscription.LastSignInAt == null)
            {
                this.NavigationPushModalPageAsync(new SubscriptionConfigurationPage(OnAppearing));
            }
            else
            {
                var storageTargetManager = Xamarin.Forms.DependencyService.Get<Managers.Facade.IFileStorageManagerFacade>();

                Master.BindingContext = new Models.Page.MasterPageModel
                {
                    IsBusy = false,
                    Subscriptor = new Models.View.SubscriptorViewModel
                    {
                        Name = subscription.Name,
                        LastName = subscription.LastName,
                        ImageSource = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IImageSourceProviderFacade>().GetByModel(subscription)
                    },
                    Target = new Models.View.StorageViewModel
                    {
                        FamilyRepresentativeIconGlyph = "\uf028",
                        CurrentUsedPercentFactor = 0.00d
                    }
                };

                (Master as MasterPage).InvalidateTargetStorageStats();

                (Master as MasterPage).PresentForStoragePick();

                PivotToHomePage();

                IsEnabled = true;
            }
        }

        public void PivotToHomePage()
        {
            PivotTo(new HomePage());
        }

        public void PivotToLibraryPage()
        {
            PivotTo(new LibraryPage());
        }

        public void PivotToSearchPage()
        {
            PivotTo(new SearchPage());
        }

        private void PivotTo(Xamarin.Forms.Page incomingPage)
        {
            var outgoingPage = Detail;

            if (Detail is Xamarin.Forms.NavigationPage)
            {
                (Detail as Xamarin.Forms.NavigationPage).PopToRootAsync(false);

                outgoingPage = (Detail as Xamarin.Forms.NavigationPage).RootPage;
            }

            Detail = new Xamarin.Forms.NavigationPage(incomingPage);

            if (outgoingPage is System.IDisposable)
            {
                (outgoingPage as System.IDisposable).Dispose();
            }
        }
    }
}
