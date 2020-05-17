namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class DatabaseSharePageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string busyProgressMessageDetail;
        private int busyProgressDetailMessage;
        private View.WiFiHostPotDetailViewModel wiFiHostPotDetail;

        public View.ProfileViewModel Profile { get; set; }

        public string BusyProgressMessageDetail { get => busyProgressMessageDetail; set => SetProperty(ref busyProgressMessageDetail, value); }

        public int BusyProgressDetailMessage { get => busyProgressDetailMessage; set => SetProperty(ref busyProgressDetailMessage, value); }
        
        public View.WiFiHostPotDetailViewModel WiFiHostPotDetail { get => wiFiHostPotDetail; set => SetProperty(ref wiFiHostPotDetail, value); }
    }
}
