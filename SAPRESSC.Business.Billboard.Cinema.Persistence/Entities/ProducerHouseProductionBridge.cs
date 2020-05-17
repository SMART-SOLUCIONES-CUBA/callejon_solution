namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class ProducerHouseProductionBridge
    {
        public long ProductionId { get; set; }

        public Production Production { get; set; }

        public long ProducerHouseId { get; set; }

        public ProducerHouse ProducerHouse { get; set; }
    }
}
