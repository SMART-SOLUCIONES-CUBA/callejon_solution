using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class HomePage : Base.FetchOrderAudioVisualPresentBaseContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.HomePageModel
            {
                IsBusy = true
            };
        }

        private Models.Page.HomePageModel BindingContextModel
        {
            get
            {
                return BindingContext as Models.Page.HomePageModel;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            System.Threading.Tasks.Task.Run(async () =>
            {
                var fetchOrderManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.IFetchOrderManagerFacade>();

                var advertisementProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IAdvertisementProviderFacade>();
                var audioVisualProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IAudioVisualProviderFacade>();

                var advertisements = await advertisementProvider.GetAllAsync();
                var popularAudioVisualsPagination = await audioVisualProvider.GetPopularsAsync(1, 10);
                var lastAudioVisualsPagination = await audioVisualProvider.GetLastsAsync(1, 10);
                var recomendedAudioVisualsPagination = await audioVisualProvider.GetRecomendedsAsync(1, 10);

                var popularAudioVisuals = popularAudioVisualsPagination.List.Select(item => new Models.View.AudioVisualViewModel
                {
                    AudioVisual = item,
                    IsMarkedForFetch = Xamarin.Forms.Application.Current.HasAudioVisualForFetch(item)
                }).ToList();

                var lastAudioVisuals = lastAudioVisualsPagination.List.Select(item => new Models.View.AudioVisualViewModel
                {
                    AudioVisual = item,
                    IsMarkedForFetch = Xamarin.Forms.Application.Current.HasAudioVisualForFetch(item)
                }).ToList();

                var recomendedAudioVisuals = recomendedAudioVisualsPagination.List.Select(item => new Models.View.AudioVisualViewModel
                {
                    AudioVisual = item,
                    IsMarkedForFetch = Xamarin.Forms.Application.Current.HasAudioVisualForFetch(item)
                }).ToList();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    BindingContextModel.Advertisements = advertisements;
                    BindingContextModel.PopularAudioVisuals = popularAudioVisuals;
                    BindingContextModel.LastAudioVisuals = lastAudioVisuals;
                    BindingContextModel.RecomendedAudioVisuals = recomendedAudioVisuals;

                    BindingContextModel.IsBusy = false;

                    IsEnabled = true;

                    Xamarin.Forms.Device.StartTimer(System.TimeSpan.FromSeconds(5), () =>
                    {
                        var nextPosition = AdvertisementsCarouselView.Position + 1;

                        AdvertisementsCarouselView.Position = nextPosition >= BindingContextModel.Advertisements.Length ? 0 : nextPosition;

                        return true;
                    });
                });
            });
        }

        private void LibraryPivotButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            (Xamarin.Forms.Application.Current.MainPage as MainPage).PivotToLibraryPage();
        }

        private void SearchPivotButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            (Xamarin.Forms.Application.Current.MainPage as MainPage).PivotToSearchPage();
        }

        private void AudioVisualItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            var model = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.AudioVisualViewModel;

            Navigation.PushAsync(new AudioVisualDetailPage(model, this));
        }

        private void AudioVisualItemFetchMarkToogleButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            try
            {
                var model = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.AudioVisualViewModel;

                if (model.IsMarkedForFetch)
                {
                    Xamarin.Forms.Application.Current.RemoveAudioVisualForFetch(model.AudioVisual);
                }
                else
                {
                    Xamarin.Forms.Application.Current.AddAudioVisualForFetch(model.AudioVisual);
                }

                OnAudioViualFetchMarkToogle(model.AudioVisual);
            }
            catch (Exceptions.UnspecifiedStorageDeviceException)
            {
                DisplayAlert(Providers.Constatnt.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, Providers.Constatnt.StringConstantProvider.UNSPECIFIED_STORAGE_DEVICE_EXCEPTION_MESSAGE, Providers.Constatnt.StringConstantProvider.OK_ACTION);
            }
            catch (Exceptions.InsufficientStorageSpaceException)
            {
                DisplayAlert(Providers.Constatnt.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, Providers.Constatnt.StringConstantProvider.INSUFFICIENT_STORAGE_SPACE_EXCEPTION_MESSAGE, Providers.Constatnt.StringConstantProvider.OK_ACTION);
            }
            catch
            {
                // TODO Handle unespected exception!
            }
        }

        public override void OnAudioViualFetchMarkToogle(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel target)
        {
            var models = BindingContextModel.PopularAudioVisuals.Where(item => item.AudioVisual.Id == target.Id).ToList();

            models.AddRange(BindingContextModel.LastAudioVisuals.Where(item => item.AudioVisual.Id == target.Id));

            models.AddRange(BindingContextModel.RecomendedAudioVisuals.Where(item => item.AudioVisual.Id == target.Id));

            foreach (var item in models)
            {
                item.IsMarkedForFetch = !item.IsMarkedForFetch;
            }
        }
    }

}