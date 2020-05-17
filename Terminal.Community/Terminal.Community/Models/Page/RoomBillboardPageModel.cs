namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class RoomBillboardPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private System.Collections.ObjectModel.ObservableCollection<View.RoomDayBillboardViewModel> dayPresentations;

        public System.Collections.ObjectModel.ObservableCollection<View.RoomDayBillboardViewModel> DayPresentations { get => dayPresentations; set => SetProperty(ref dayPresentations, value); }

        public View.RoomViewModel Room { get; set; }
    }
}
