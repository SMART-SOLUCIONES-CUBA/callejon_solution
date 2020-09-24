namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Views.Wrapper
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class HorizontalPosterWrapperView : Xamarin.Forms.ContentView
    {
        public HorizontalPosterWrapperView(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel model)
        {
            InitializeComponent();

            BindingContext = model;
        }
    }
}