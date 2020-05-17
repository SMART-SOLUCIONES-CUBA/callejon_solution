namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CoCountryDisplayViewModel
    {
        public long ProductionId { get; set; }

        public ProductionDisplayViewModel ProductionDisplay { get; set; }

        public int CountryId { get; set; }

        public Middleware.Region.Persistence.Entities.Country Country { get; set; }
    }
}
