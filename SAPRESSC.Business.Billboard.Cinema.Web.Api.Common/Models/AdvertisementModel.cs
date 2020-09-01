namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class AdvertisementModel
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public ImageModel PlacardImage { get; set; }

        public string About { get; set; }

        public string Action { get; set; }
    }
}
