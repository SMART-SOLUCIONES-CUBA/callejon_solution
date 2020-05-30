namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class DatabaseShareRecivePageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        public DatabaseShareRecivePageModel()
        {
            Presences = new System.Collections.ObjectModel.ObservableCollection<Data.PresenceDataModel>();
        }

        public System.Collections.ObjectModel.ObservableCollection<Data.PresenceDataModel> Presences { get; set; }
    }
}
