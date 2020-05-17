using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class WithInInputViewModel
    {
        public System.Guid EventId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Personalidad")]
        public long PersonalityId { get; set; }

        public ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Personalities;

        public bool IsPresenter { get; set; }

    }
}
