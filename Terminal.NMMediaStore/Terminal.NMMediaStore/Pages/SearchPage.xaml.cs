namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class SearchPage : Xamarin.Forms.ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.SearchPageModel
            {
                IsBusy = true
            };
        }

        private Models.Page.SearchPageModel BindingContextModel
        {
            get
            {
                return BindingContext as Models.Page.SearchPageModel;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!IsEnabled)
            {
                System.Threading.Tasks.Task.Run(async () =>
                {
                    var categoryProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.ICategoryProviderFacade>();
                    var producerHouseProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IProducerHouseProviderFacade>();
                    var yearProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IYearProviderFacade>();
                    var countryProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.ICountryProviderFacade>();

                    var categories = await categoryProvider.GetByParentAsync(null);
                    var producerHouses = await producerHouseProvider.GetAllAsync();
                    var years = await yearProvider.GetAllAsync();
                    var countries = await countryProvider.GetAllAsync();

                    await LoadSearchsAsync();

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        BindingContextModel.PickableCategories = categories;
                        BindingContextModel.PickableYears = years;
                        BindingContextModel.PickableProducerHouses = producerHouses;
                        BindingContextModel.PickableCountries = countries;

                        BindingContextModel.IsBusy = false;

                        IsEnabled = true;
                    });
                });
            }
            else
            {
                BindingContextModel.IsBusy = true;

                System.Threading.Tasks.Task.Run(async () =>
                {
                    await LoadSearchsAsync();
                }).ContinueWith(_ =>
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        BindingContextModel.IsBusy = false;
                    });
                });
            }
        }

        private void HomePivotButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            (Xamarin.Forms.Application.Current.MainPage as MainPage).PivotToHomePage();
        }

        private void LibraryPivotButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            (Xamarin.Forms.Application.Current.MainPage as MainPage).PivotToLibraryPage();
        }

        private void SearchFormResetButton_Click(object sender, System.EventArgs eventArgs)
        {
            DisplayAlert("Confirmar", "Está seguro que desea recomenzar la definición de su búsqueda?", "Recomenzar", "Cancelar").ContinueWith(task =>
            {
                if (!task.Result) return;

                BindingContextModel.TitleTemathic = null;
                BindingContextModel.Category = null;
                BindingContextModel.Country = null;
                BindingContextModel.ProducerHouse = null;
                BindingContextModel.Year = null;
                BindingContextModel.Genres = null;
                BindingContextModel.Personalities = null;
                BindingContextModel.Tags = null;
            });
        }

        private void SearchFormCommitButton_Click(object sender, System.EventArgs eventArgs)
        {
            var subscriptionManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>();

            Navigation.PushAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                SubscriptionProfile = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel
                {
                    Id = subscriptionManager.GetAuthenticatedSubscritor().Subscription.Id
                },
                TitleTemathic = BindingContextModel.TitleTemathic,
                Category = BindingContextModel.Category,
                Country = BindingContextModel.Country,
                ProducerHouse = BindingContextModel.ProducerHouse,
                Year = BindingContextModel.Year ?? 0,
                Genres = BindingContextModel.Genres?.ToArray(),
                Personalities = BindingContextModel.Personalities?.ToArray(),
                Tags = BindingContextModel.Tags?.ToArray()
            }));
        }

        private void SearchItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            Navigation.PushAsync(new FilterResultPage((sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel));
        }

        private async System.Threading.Tasks.Task LoadSearchsAsync()
        {
            var subscriptionManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>();
            var searchProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.ISearchProviderFacade>();

            var subscriptor = subscriptionManager.GetAuthenticatedSubscritor();

            var subscriptorSearches = await searchProvider.GetLastSubscriptorSearchesAsync(subscriptor.Subscription.Id);
            var otherSubscriptorsSearches = await searchProvider.GetLastOtherSubscriptorsSearchesAsync(subscriptor.Subscription.Id);

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                BindingContextModel.SubscriptorSearches = subscriptorSearches;
                BindingContextModel.OtherSubscriptorsSearches = otherSubscriptorsSearches;
            });
        }
    }
}