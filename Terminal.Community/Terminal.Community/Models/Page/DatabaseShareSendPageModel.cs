using SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class DatabaseShareSendPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private bool hasFirendReceivers;
        private bool isSendingBundle;
        private double bundleSendProgress;
        private WiFiHostpotDetailViewModel wiFiHostpotDetail;

        public DatabaseShareSendPageModel()
        {
            Presences = new System.Collections.ObjectModel.ObservableCollection<DataShareReceiverPresenceViewModel>();
        }

        public System.Collections.ObjectModel.ObservableCollection<DataShareReceiverPresenceViewModel> Presences { get; set; }

        public ProfileViewModel Profile { get; set; }

        public bool IsSendingBundle { get => isSendingBundle; set => SetProperty(ref isSendingBundle, value); }

        public double BundleSendProgress { get => bundleSendProgress; set => SetProperty(ref bundleSendProgress, value); }
        
        public WiFiHostpotDetailViewModel WiFiHostpotDetail { get => wiFiHostpotDetail; set => SetProperty(ref wiFiHostpotDetail, value); }

        public bool HasFirendReceivers { get => hasFirendReceivers; set => SetProperty(ref hasFirendReceivers, value); }
    }
}
