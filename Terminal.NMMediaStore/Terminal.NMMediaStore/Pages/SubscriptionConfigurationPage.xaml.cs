using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class SubscriptionConfigurationPage : Xamarin.Forms.CarouselPage
    {
        private System.Action callbackAction;

        public SubscriptionConfigurationPage(System.Action callbackAction)
        {
            InitializeComponent();

            this.callbackAction = callbackAction;

            IsEnabled = false;

            BindingContext = new Models.Page.SubscriptionConfigurationPageModel
            {
                IsBusy = true
            };
        }

        private Models.Page.SubscriptionConfigurationPageModel BindingContextModel
        {
            get
            {
                return BindingContext as Models.Page.SubscriptionConfigurationPageModel;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            System.Threading.Tasks.Task.Run(async () =>
            {
                var producerHouseProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IProducerHouseProviderFacade>();
                var genreProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IGenreProviderFacade>();
                var tagProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.ITagProviderFacade>();

                var producerHouses = await producerHouseProvider.GetAllAsync();
                var genres = await genreProvider.GetAllAsync();
                var tags = await tagProvider.GetAllAsync();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    BindingContextModel.ProducerHouseChoises = producerHouses.Select(item => new Models.View.ProducerHouseChoiseViewModel
                    {
                        ProducerHouse = item
                    }).ToArray();
                    BindingContextModel.GenreChoises = genres.Select(item => new Models.View.GenreChoiseViewModel
                    {
                        Genre = item
                    }).ToArray();
                    BindingContextModel.TagChoises = tags.Select(item => new Models.View.TagChoiseViewModel
                    {
                        Tag = item
                    }).ToArray();

                    BindingContextModel.IsBusy = false;

                    IsEnabled = true;
                });
            });
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if ((Children.Count != 5) || (Children.IndexOf(CurrentPage) < Children.Count - 1)) return;

            BindingContextModel.IsBusy = true;

            System.Threading.Tasks.Task.Run(() =>
            {
                var choisedGenres = BindingContextModel.GenreChoises.Where(item => item.Choised).Select(item => item.Genre).ToArray();
                var choisedProducerHouses = BindingContextModel.ProducerHouseChoises.Where(item => item.Choised).Select(item => item.ProducerHouse).ToArray();
                var choisedTags = BindingContextModel.TagChoises.Where(item => item.Choised).Select(item => item.Tag).ToArray();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    BindingContextModel.ChoisedGenres = choisedGenres;
                    BindingContextModel.ChoisedProducerHouses = choisedProducerHouses;
                    BindingContextModel.ChoisedTags = choisedTags;

                    BindingContextModel.IsBusy = false;
                });
            });
        }

        private void CompleteButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            BindingContextModel.IsBusy = true;

            var subscriptionManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>();

            subscriptionManager.PersistSubscriptionAsync(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel
            {
                PreferedGenres = BindingContextModel.GenreChoises.Where(item => item.Choised).Select(item => item.Genre).ToArray(),
                PreferedProducerHouses = BindingContextModel.ProducerHouseChoises.Where(item => item.Choised).Select(item => item.ProducerHouse).ToArray(),
                PreferedTags = BindingContextModel.TagChoises.Where(item => item.Choised).Select(item => item.Tag).ToArray()
            }).ContinueWith(persistTask =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (persistTask.Exception == null)
                    {
                        Navigation.PopModalAsync();

                        callbackAction?.Invoke();

                        callbackAction = null;
                    }
                    else
                    {
                        // TODO Handle exception !!

                        DisplayAlert("Aviso", "Exception no gestionada.", "Aceptar");
                    }

                    BindingContextModel.IsBusy = false;
                });
            });
        }

        private void NextButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var index = Children.IndexOf(CurrentPage);

            CurrentPage = Children[index + 1];
        }

        private void PreviousButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var index = Children.IndexOf(CurrentPage);

            CurrentPage = Children[index - 1];
        }
    }
}