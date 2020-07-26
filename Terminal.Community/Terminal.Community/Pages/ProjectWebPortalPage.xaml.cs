namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ProjectWebPortalPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public ProjectWebPortalPage()
        {
            InitializeComponent();
        }

        private void GoButton_Clicked(object sender, System.EventArgs e)
        {
            Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("http://www.callejon.cmg.cu"));
        }
    }
}