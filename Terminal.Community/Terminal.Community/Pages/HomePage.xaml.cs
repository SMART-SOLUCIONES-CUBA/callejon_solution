namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class HomePage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void CinemaBillboardRecomendation_Tapped(object sender, System.EventArgs e)
        {
            ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).PresentCinemaBillboardSection();
        }

        private void EventsRecommendation_Tapped(object sender, System.EventArgs e)
        {
            ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).PresentEventsSection();
        }
    }
}