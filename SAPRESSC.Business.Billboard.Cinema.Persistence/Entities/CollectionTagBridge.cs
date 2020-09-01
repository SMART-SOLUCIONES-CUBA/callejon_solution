namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class CollectionTagBridge
    {
        public System.Guid CollectionId { get; set; }

        public Collection Collection { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
