using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class LibraryPage : XFToolKit.Mvvm.Pages.ContentPage
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
                var relevantProducerHouses = await producerHouseProvider.GetRelevantsAsync(11);
                var relevantGenres = await genreProvider.GetRelevantsAsync(11);
                var relevantYears = await yearProvider.GetRelevantsAsync(11);
                var relevantPerformancePersonalities = await personalityProvider.GetRelevantActorsAsync(21);
                var relevantDirectionPersonalities = await personalityProvider.GetRelevantDirectorsAsync(21);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    BindingContextModel.Categories = categories;
                    BindingContextModel.RelevantProducerHouses = relevantProducerHouses.Take(10).ToArray();
                    BindingContextModel.HasMoreProducerHouses = relevantProducerHouses.Length > 10;
                    BindingContextModel.RelevantGenres = relevantGenres.Take(10).ToArray();
                    BindingContextModel.HasMoreGenres = relevantGenres.Length > 10;
                    BindingContextModel.RelevantYears = relevantYears.Take(10).ToArray();
                    BindingContextModel.HasMoreYears = relevantYears.Length > 10;
                    BindingContextModel.RelevantPerformancePersonalities = relevantPerformancePersonalities.Distinct().ToArray();
                    BindingContextModel.HasMorePerformancePersonalities = relevantPerformancePersonalities.Length > 20; 
                    BindingContextModel.RelevantDirectionPersonalities = relevantDirectionPersonalities.Distinct().ToArray();
                    BindingContextModel.HasMoreDirectionPersonalities = relevantDirectionPersonalities.Length > 20;

                    BindingContextModel.IsBusy = false;

                    IsEnabled = true;
                });
            }).ContinueWith(filterTask =>
            {
                if (filterTask.Exception != null || filterTask.IsCanceled)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(filterTask.Exception);
                    });
                }
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
            this.NavigationPushPageAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                SubscriptionId = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>().GetAuthenticatedSubscritor().Subscription.Id,
                Category = (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel
            }));
        }

        private void AllProducerHousesButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new ProducerHousesPage());
        }

        private void ProducerHouseItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                SubscriptionId = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>().GetAuthenticatedSubscritor().Subscription.Id,
                ProducerHouse = (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel
            }));
        }

        private void AllGenresButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new GenresPage());
        }

        private void GenreItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                SubscriptionId = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>().GetAuthenticatedSubscritor().Subscription.Id,
                Genres = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[] { (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel }
            }));
        }

        private void AllYearsButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new YearsPage());
        }

        private void YearItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                SubscriptionId = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>().GetAuthenticatedSubscritor().Subscription.Id,
                Year = (int)(sender as Xamarin.Forms.BindableObject).BindingContext
            }));
        }

        private void AllPerformancePersonalitiesButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new PerformancePersonalitiesPage());
        }

        private void AllDirectionPersonalitiesButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new DirectionPersonalitiesPage());
        }

        private void PersonalityItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                SubscriptionId = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>().GetAuthenticatedSubscritor().Subscription.Id,
                Personalities = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.PersonalityModel[] { (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.PersonalityModel }
            }));
        }
    }
}