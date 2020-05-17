namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class EventsPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private System.Collections.ObjectModel.ObservableCollection<View.DayEventsViewModel> dayEvents;

        public System.Collections.ObjectModel.ObservableCollection<View.DayEventsViewModel> DayEvents { get => dayEvents; set => SetProperty(ref dayEvents, value); }
    }
}
