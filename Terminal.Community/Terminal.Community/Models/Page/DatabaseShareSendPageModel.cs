using SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class DatabaseShareSendPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string busyProgressMessageDetail;
        private int busyProgressDetailMessage;
        private WiFiHostpotDetailViewModel wiFiHostpotDetail;

        public DatabaseShareSendPageModel()
        {
            Presences = new System.Collections.ObjectModel.ObservableCollection<Data.PresenceDataModel>();
        }

        public System.Collections.ObjectModel.ObservableCollection<Data.PresenceDataModel> Presences { get; set; }

        public ProfileViewModel Profile { get; set; }

        public string BusyProgressMessageDetail { get => busyProgressMessageDetail; set => SetProperty(ref busyProgressMessageDetail, value); }

        public int BusyProgressDetailMessage { get => busyProgressDetailMessage; set => SetProperty(ref busyProgressDetailMessage, value); }
        
        public WiFiHostpotDetailViewModel WiFiHostpotDetail { get => wiFiHostpotDetail; set => SetProperty(ref wiFiHostpotDetail, value); }
    }
}
