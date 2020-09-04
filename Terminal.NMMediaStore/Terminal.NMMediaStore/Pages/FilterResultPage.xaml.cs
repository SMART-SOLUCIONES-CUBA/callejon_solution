using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class FilterResultPage : Base.FetchOrderAudioVisualPresentBaseContentPage
    {
        public FilterResultPage(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel model)
        {
            InitializeComponent();

            BindingContext = new Models.Page.FilterResultPageModel
            {
                Search = model,
                IsSearchSummaryExpanded = false
            };

            IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            LoadNextPageSearchResults();

            IsEnabled = true;
        }

        private Models.Page.FilterResultPageModel BindingContextModel => BindingContext as Models.Page.FilterResultPageModel;

        private void AlternativeBackButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            Navigation.PopAsync();
        }

        private void AudioVisualItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            var model = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.AudioVisualViewModel;

            Navigation.PushAsync(new AudioVisualDetailPage(model, this));
        }

        private void LoadNextPageButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            LoadNextPageSearchResults();
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
            catch (System.InvalidOperationException)
            {
                DisplayAlert("Aviso", "Espacio de almacenamiento disponible en su dispositivo no será suficiente para descargar este audio visual.", "Aceptar");
            }
            catch
            {
                // TODO Handle unespected exception!
            }
        }

        public override void OnAudioViualFetchMarkToogle(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel target)
        {
            var models = BindingContextModel.Results.Where(item => item.AudioVisual.Id == target.Id).ToArray();

            foreach (var item in models)
            {
                item.IsMarkedForFetch = !item.IsMarkedForFetch;
            }
        }

        private void LoadNextPageSearchResults()
        {
            BindingContextModel.IsBusy = true;
            BindingContextModel.PaginationHasNext = false;

            System.Threading.Tasks.Task.Run(async () =>
            {
                var audioVisualProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IAudioVisualProviderFacade>();

                var nextPage = BindingContextModel.PaginationCurrentPage + 1;

                var result = await audioVisualProvider.SearchAsync(BindingContextModel.Search, nextPage);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var fetchOrderManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.IFetchOrderManagerFacade>();

                    if (BindingContextModel.Results == null)
                    {
                        BindingContextModel.Results = new System.Collections.ObjectModel.ObservableCollection<Models.View.AudioVisualViewModel>();
                    }

                    if (result.List.Count > 0)
                    {
                        foreach (var item in result.List)
                        {
                            BindingContextModel.Results.Add(new Models.View.AudioVisualViewModel
                            {
                                AudioVisual = item,
                                IsMarkedForFetch = Xamarin.Forms.Application.Current.HasAudioVisualForFetch(item)
                            });
                        }

                        BindingContextModel.PaginationHasNext = result.HasNext;
                    }
                    else
                    {
                        BindingContextModel.PaginationHasNext = false;
                    }

                    BindingContextModel.IsBusy = false;
                });
            });
        }
    }
}