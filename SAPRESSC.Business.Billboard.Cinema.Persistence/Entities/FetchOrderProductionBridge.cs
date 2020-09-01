namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class FetchOrderProductionBridge
    {
        public long FetchOrderId { get; set; }

        public FetchOrder FetchOrder { get; set; }

        public long ProductionId { get; set; }

        public Production Production { get; set; }
    }
}
