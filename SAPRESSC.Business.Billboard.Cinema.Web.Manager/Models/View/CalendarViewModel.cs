namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CalendarViewModel
    {
        public Filter.CalendarFilterModel Filter { get; set; }

        public System.Collections.Generic.ICollection<DailyPresentationsBillboardViewModel> DailyPresentationsBillboards { get; set; }
    }
}
