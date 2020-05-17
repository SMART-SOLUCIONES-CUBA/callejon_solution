namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class ProductionParticipationsManagementViewModel
    {
        public ProductionDisplayViewModel Display { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Personalities { get; set; }

        public System.Collections.Generic.ICollection<Persistence.Entities.Participation> Participations { get; set; }

        public System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ProductionRoles { get; set; }
    }
}
