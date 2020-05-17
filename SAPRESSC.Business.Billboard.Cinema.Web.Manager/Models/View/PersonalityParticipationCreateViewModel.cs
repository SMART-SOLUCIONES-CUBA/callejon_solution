using Microsoft.AspNetCore.Mvc.Rendering;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class PersonalityParticipationCreateViewModel
    {
        public PersonalityParticipationCreateViewModel()
        {
            ProductionRoles = new List<SelectListItem>
            {
                new SelectListItem { Value = ProductionRole.Production.ToString(), Text = ProductionRole.Production.ToString() },
                new SelectListItem { Value = ProductionRole.Direction.ToString(), Text = ProductionRole.Direction.ToString() },
                new SelectListItem { Value = ProductionRole.Interpretation.ToString(), Text = ProductionRole.Interpretation.ToString() },
                new SelectListItem { Value = ProductionRole.Screenplay.ToString(), Text = ProductionRole.Screenplay.ToString() },
                new SelectListItem { Value = ProductionRole.Photography.ToString(), Text = ProductionRole.Photography.ToString() },
                new SelectListItem { Value = ProductionRole.Sound.ToString(), Text =  ProductionRole.Sound.ToString() },
                new SelectListItem { Value = ProductionRole.Edition.ToString(), Text = ProductionRole.Edition.ToString() },
                new SelectListItem { Value = ProductionRole.ArtisticManagement.ToString(), Text =  ProductionRole.ArtisticManagement.ToString() }
            };
        }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "Sex")]
        public Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "BornDate")]
        public System.DateTime BornDate { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Required]
        public ProductionRole ProductionRole { get; set; }

        public IEnumerable<SelectListItem> ProductionRoles { get; set; }

        public long ProductionId { get; set; }

        public string Specification { get; set; }
    }
}
