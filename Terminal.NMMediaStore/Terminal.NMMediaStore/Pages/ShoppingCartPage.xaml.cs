using Xamarin.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ShoppingCartPage : ContentPage
    {
        public ShoppingCartPage()
        {
            InitializeComponent();

            BindingContext = (Application.Current.MainPage as MasterDetailPage).Master.BindingContext;
        }

        private Models.Page.MasterPageModel BindingContextModel
        {
            get
            {
                return BindingContext as Models.Page.MasterPageModel;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            TranslationX = Width * -1;

            var animation = new XFToolKit.Presentation.Transform.Animations.TranslateToXAnimation
            {
                EasingType = XFToolKit.Presentation.Transform.Enumerations.EasingType.CubicOut,
                Target = this,
                TranslateX = 0,
                Duration = "500"
            };

            animation.Begin();
        }

        private void BackButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            Navigation.PopModalAsync();
        }

        private void FetchOrderAudioVisualItemRemoveButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            DisplayAlert("Confirmar", "Esta seguro que desea eliminar este Audio Visual del Carro de Compras?", "Eliminar", "Cancelar").ContinueWith(task =>
            {
                if (!task.Result) return;

                Device.BeginInvokeOnMainThread(() =>
                {
                    var model = (sender as BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel;

                    BindingContextModel.FetchOrderAudioVisuals.Remove(model);

                    ((Application.Current.MainPage as MasterDetailPage).Master as Pages.MasterPage).InvalidateTargetStorageState();

                    TargetStorageVirtualTotalSpaceBoxView.InvalidateWidth();
                    VirtualStoreUsedSpaceSpan.InvalidateText();

                    BindingContextModel.RaiseFetchOrderAudioVisualsChanged();

                    var presentedPage = ((Application.Current.MainPage as MasterDetailPage).Detail as NavigationPage).CurrentPage;

                    if (presentedPage is Base.FetchOrderAudioVisualPresentBaseContentPage)
                    {
                        (presentedPage as Base.FetchOrderAudioVisualPresentBaseContentPage).OnAudioViualFetchMarkToogle(model);
                    }
                });
            });
        }
    }
}