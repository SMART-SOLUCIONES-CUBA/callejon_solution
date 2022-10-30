using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class SubscriptionCreateViewModel : SubscriptionInputViewModel
    {
        [Required(ErrorMessage = "RequiredValidationErrorMessage"), StringLength(30, MinimumLength = 6, ErrorMessage = "StringLengthMinMaxValidationErrorMessage"), DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password", ErrorMessage = "CompareValidationErrorMessage")]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public override Persistence.Entities.Subscription Export()
        {
            var entity = base.Export();

            entity.Password = Password;

            return entity;
        }
    }
}