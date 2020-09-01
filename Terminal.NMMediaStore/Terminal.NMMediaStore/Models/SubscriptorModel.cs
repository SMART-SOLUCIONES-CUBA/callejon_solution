namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models
{
    public class SubscriptorModel
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel Subscription { get; set; }
    }
}