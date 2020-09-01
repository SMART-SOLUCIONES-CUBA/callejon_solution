namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class LibraryPage : Xamarin.Forms.ContentPage
    {
        public LibraryPage()
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.LibraryPageModel
            {
                IsBusy = true
            };
        }

        private Models.Page.LibraryPageModel BindingContextModel
        {
            get
            {
                return BindingContext as Models.Page.LibraryPageModel;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            System.Threading.Tasks.Task.Run(async () =>
            {
                var categoryProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.ICategoryProviderFacade>();
                var producerHouseProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IProducerHouseProviderFacade>();
                var genreProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IGenreProviderFacade>();
                var yearProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IYearProviderFacade>();
                var personalityProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IPersonalityProviderFacade>();

                var categories = await categoryProvider.GetByParentAsync(null);
                var relevantProducerHouses = await producerHouseProvider.GetRelevantsAsync();
                var relevantGenres = await genreProvider.GetRelevantsAsync();
                var relevantYears = await yearProvider.GetRelevantAsync();
                var relevantPerformancePersonalities = await personalityProvider.GetRelevantActorsAsync();
                var relevantDirectionPersonalities = await personalityProvider.GetRelevantDirectorsAsync();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    BindingContextModel.Categories = categories;
                    BindingContextModel.RelevantProducerHouses = relevantProducerHouses;
                    BindingContextModel.RelevantGenres = relevantGenres;
                    BindingContextModel.RelevantYears = relevantYears;
                    BindingContextModel.RelevantPerformancePersonalities = relevantPerformancePersonalities;
                    BindingContextModel.RelevantDirectionPersonalities = relevantDirectionPersonalities;

                    BindingContextModel.IsBusy = false;

                    IsEnabled = true;
                });
            });
        }

        private void HomePivotButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            (Xamarin.Forms.Application.Current.MainPage as MainPage).PivotToHomePage();
        }

        private void SearchPivotButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            (Xamarin.Forms.Application.Current.MainPage as MainPage).PivotToSearchPage();
        }

        private void CategoryItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            Navigation.PushAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                Category = (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel
            }));
        }

        private void ProducerHouseItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            Navigation.PushAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                ProducerHouse = (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel
            }));
        }

        private void GenreItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            Navigation.PushAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                Genres = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[] { (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel }
            }));
        }

        private void YearItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            Navigation.PushAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                Year = (int)(sender as Xamarin.Forms.BindableObject).BindingContext
            }));
        }

        private void PersonalityItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            Navigation.PushAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                Personalities = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.PersonalityModel[] { (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.PersonalityModel }
            }));
        }
    }
}