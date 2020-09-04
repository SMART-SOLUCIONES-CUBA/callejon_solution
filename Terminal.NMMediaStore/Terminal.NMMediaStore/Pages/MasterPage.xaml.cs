using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class MasterPage : Xamarin.Forms.ContentPage
    {
        private const string FETCH_ORDER_AUDIO_VISUAL_OPTION_DETAIL = "Mostrar Detalles";
        private const string FETCH_ORDER_AUDIO_VISUAL_OPTION_QUIT = "Quitar del Carro de Compras";

        public MasterPage()
        {
            InitializeComponent();
        }

        public void InvalidateTargetStorageState()
        {
            StorageSpaceReferenceBoxView.InvalidateWidth();
        }

        private void FullShoppingCartButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage).Navigation.PushModalAsync(new ShoppingCartPage(), false);
        }

        private void FetchOrderAudioVisualOptionButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var model = (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel;

            DisplayActionSheet("Opciones", "Cancelar", null, FETCH_ORDER_AUDIO_VISUAL_OPTION_DETAIL, FETCH_ORDER_AUDIO_VISUAL_OPTION_QUIT).ContinueWith(task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    switch (task.Result)
                    {
                        case FETCH_ORDER_AUDIO_VISUAL_OPTION_DETAIL:
                            var detailNavigationPage = (Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage;

                            if (detailNavigationPage.CurrentPage is AudioVisualDetailPage)
                            {
                                if ((detailNavigationPage.CurrentPage as AudioVisualDetailPage).BindingContextModel.AudioVisual.Id == model.Id)
                                {
                                    break;
                                }
                            }

                            detailNavigationPage.PushAsync(new AudioVisualDetailPage(new Models.View.AudioVisualViewModel
                            {
                                AudioVisual = model,
                                IsMarkedForFetch = true
                            }, detailNavigationPage.CurrentPage as Base.FetchOrderAudioVisualPresentBaseContentPage));

                            break;

                        case FETCH_ORDER_AUDIO_VISUAL_OPTION_QUIT:
                            Xamarin.Forms.Application.Current.RemoveAudioVisualForFetch(model);

                            var presentedPage = ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage).CurrentPage;

                            if (presentedPage is Base.FetchOrderAudioVisualPresentBaseContentPage)
                            {
                                (presentedPage as Base.FetchOrderAudioVisualPresentBaseContentPage).OnAudioViualFetchMarkToogle(model);
                            }

                            break;
                    }
                });
            });            
        }
    }
}