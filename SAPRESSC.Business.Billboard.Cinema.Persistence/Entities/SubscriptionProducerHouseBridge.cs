namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class SubscriptionProducerHouseBridge
    {
        public string SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }

        public long ProducerHouseId { get; set; }

        public ProducerHouse ProducerHouse { get; set; }
    }
}
