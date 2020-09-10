namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class SubscriptionGenreBridge
    {
        public string SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
