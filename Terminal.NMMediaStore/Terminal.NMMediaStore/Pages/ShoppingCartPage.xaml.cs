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
    }
}