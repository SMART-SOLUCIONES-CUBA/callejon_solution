using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CoCountryInputViewModel
    {
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Countries { get; set; }

        public long ProductionId { get; set; }
    }
}
