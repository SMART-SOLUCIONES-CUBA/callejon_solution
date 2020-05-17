namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class CountryCoProductionBridge
    {
        public long ProductionId { get; set; }

        public Production Production { get; set; }

        public int CountryId { get; set; }

        public Middleware.Region.Persistence.Entities.Country Country { get; set; }
    }
}
