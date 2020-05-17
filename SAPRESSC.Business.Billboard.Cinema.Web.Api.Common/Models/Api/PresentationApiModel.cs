namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api
{
    public class PresentationApiModel
    {
        public long Id { get; set; }

        public System.DateTime Date { get; set; }

        public ProductionApiModel Production { get; set; }
    }
}
