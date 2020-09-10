namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class SubscriptionTagBridge
    {
        public string SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
