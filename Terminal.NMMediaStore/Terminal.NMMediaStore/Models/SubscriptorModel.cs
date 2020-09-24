namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models
{
    public class SubscriptorModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public byte[] ThumbnailContent { get; set; }

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel Subscription { get; set; }
    }
}