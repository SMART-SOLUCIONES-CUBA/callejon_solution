using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
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

            var subscriptionManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>();

            var subscriptor = subscriptionManager.GetAuthenticatedSubscritor();
            
            if (subscriptor == null)
            {
                Navigation.PushModalAsync(new SignInPage(OnAppearing));
            }
            else if (subscriptor.Subscription == null)
            {
                Navigation.PushModalAsync(new SubscriptionConfigurationPage(OnAppearing));
            }
            else
            {
                var storageTargetManager = Xamarin.Forms.DependencyService.Get<Managers.Facade.IStorageTargetManagerFacade>();

                var storage = storageTargetManager.GetStorageTarget();

                // TODO Remove mock code block sections!!

                Master.BindingContext = new Models.Page.MasterPageModel
                {
                    IsBusy = false,
                    Subscriptor = new Models.View.SubscriptorViewModel
                    {
                        Name = subscriptor.Name,
                        LastName = subscriptor.LastName,
                        Image = subscriptor.Subscription.Image
                    },
                    Target = new Models.View.StorageViewModel
                    {
                        Storage = storage,
                        FamilyRepresentativeIconGlyph = storage?.Family.ToFamilyRepresentativeIconGlyph() ?? "\uf028",
                        CurrentUsedPercentFactor = storage?.UsedSpace / storage?.TotalSpace ?? 0.00d
                    }
                };

                (Master as MasterPage).InvalidateTargetStorageState();

                PivotToHomePage();

                IsEnabled = true;
            }
        }

        public void PivotToHomePage()
        {
            Detail = new Xamarin.Forms.NavigationPage(new HomePage());
        }

        public void PivotToLibraryPage()
        {
            Detail = new Xamarin.Forms.NavigationPage(new LibraryPage());
        }

        public void PivotToSearchPage()
        {
            Detail = new Xamarin.Forms.NavigationPage(new SearchPage());
        }
    }
}
