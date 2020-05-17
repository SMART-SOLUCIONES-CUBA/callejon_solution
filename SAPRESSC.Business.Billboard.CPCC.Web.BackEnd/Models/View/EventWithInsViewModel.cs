namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class EventWithInsViewModel
    {
        public Persistence.Entities.Event Event { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Personalities { get; set; }
    }
}
