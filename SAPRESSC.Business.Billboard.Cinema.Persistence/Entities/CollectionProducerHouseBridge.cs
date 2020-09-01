namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class CollectionProducerHouseBridge
    {
        public System.Guid CollectionId { get; set; }

        public Collection Collection { get; set; }

        public long ProducerHouseId { get; set; }

        public ProducerHouse ProducerHouse { get; set; }
    }
}
