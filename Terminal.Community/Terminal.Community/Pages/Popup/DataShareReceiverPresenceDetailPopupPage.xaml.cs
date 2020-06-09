namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages.Popup
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DataShareReceiverPresenceDetailPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public DataShareReceiverPresenceDetailPopupPage(Models.View.DataShareReceiverPresenceViewModel model)
        {
            InitializeComponent();

            BindingContext = model;
        }

        private void CloseButton_Clicked(object sender, System.EventArgs args)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
    }
}
