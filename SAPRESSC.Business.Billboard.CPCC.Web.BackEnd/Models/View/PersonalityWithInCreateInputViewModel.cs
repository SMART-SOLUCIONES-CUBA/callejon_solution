using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class PersonalityWithInCreateInputViewModel
    {
        public System.Guid EventId { get; set; }

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

        public bool IsPresenter { get; set; }
    }
}
