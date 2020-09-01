namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class FetchOrderModel
    {
        public long Id { get; set; }

        public System.DateTime At { get; set; }

        public string SubscriptorId { get; set; }

        public System.Collections.Generic.List<CollectionModel> Collections { get; set; }

        public System.Collections.Generic.List<AudioVisualModel> AudioVisuals { get; set; }
    }
}