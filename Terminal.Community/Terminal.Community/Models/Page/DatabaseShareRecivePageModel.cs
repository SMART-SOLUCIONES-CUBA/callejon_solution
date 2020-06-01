namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class DatabaseShareRecivePageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        public DatabaseShareRecivePageModel()
        {
            Friends = new System.Collections.ObjectModel.ObservableCollection<Data.FriendDataModel>();
        }

        public System.Collections.ObjectModel.ObservableCollection<Data.FriendDataModel> Friends { get; set; }
    }
}
