namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class ProductionTagBridge
    {
        public long ProductionId { get; set; }

        public Production Production { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
