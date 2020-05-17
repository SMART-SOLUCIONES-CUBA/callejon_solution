namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class ProductionCoCountriesManagementViewModel
    {
        public ProductionDisplayViewModel Display { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> CountrySelecItems { get; set; }

        public System.Collections.Generic.ICollection<Middleware.Region.Persistence.Entities.Country> Countries { get; set; }
    }
}
