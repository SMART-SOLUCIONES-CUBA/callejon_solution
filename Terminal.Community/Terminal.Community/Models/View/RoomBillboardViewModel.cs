namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class RoomBillboardViewModel
    {
        public RoomBillboardViewModel()
        {
            DayBillboards = new System.Collections.Generic.List<RoomDayBillboardViewModel>();
        }

        public string CinemaName { get; set; }

        public string RoomName { get; set; }

        public System.Collections.Generic.List<RoomDayBillboardViewModel> DayBillboards { get; set; }
    }
}
