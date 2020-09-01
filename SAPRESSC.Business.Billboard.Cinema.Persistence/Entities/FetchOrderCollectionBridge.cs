namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class FetchOrderCollectionBridge
    {
        public long FetchOrderId { get; set; }

        public FetchOrder FetchOrder { get; set; }

        public System.Guid CollectionId { get; set; }

        public Collection Collection { get; set; }
    }
}
