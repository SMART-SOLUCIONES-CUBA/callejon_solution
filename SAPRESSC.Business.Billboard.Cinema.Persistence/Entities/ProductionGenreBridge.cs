namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class ProductionGenreBridge
    {
        public long ProductionId { get; set; }

        public Production Production { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
