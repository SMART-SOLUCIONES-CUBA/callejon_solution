namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class PresentationViewModel
    {
        public long Id { get; set; }

        public System.DateTime PresentationDate { get; set; }

        public string CinemaName { get; set; }

        public string RoomName { get; set; }

        public ProductionViewModel Production { get; set; }
    }
}
