using SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class DatabaseShareSendPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private int bundleSendProgress;
        private WiFiHostpotDetailViewModel wiFiHostpotDetail;

        public DatabaseShareSendPageModel()
        {
            Presences = new System.Collections.ObjectModel.ObservableCollection<DataShareReceiverPresenceViewModel>();
        }

        public System.Collections.ObjectModel.ObservableCollection<DataShareReceiverPresenceViewModel> Presences { get; set; }

        public ProfileViewModel Profile { get; set; }

        public int BundleSendProgress { get => bundleSendProgress; set => SetProperty(ref bundleSendProgress, value); }
        
        public WiFiHostpotDetailViewModel WiFiHostpotDetail { get => wiFiHostpotDetail; set => SetProperty(ref wiFiHostpotDetail, value); }
    }
}
