using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class PhoneContactInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<PhoneContactItem, long>
    {
        public long Id { get; set; }

        [Display(Name = "Label")]
        public string Label { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Phone(ErrorMessage = "PhoneValidationErrorMessage")]
        [Display(Name = "Number")]
        public string Number { get; set; }

        public long CinemaId { get; set; }

        public PhoneContactItem Export()
        {
            var entity = new PhoneContactItem();

            Mergue(entity);

            return entity;
        }

        public void Import(PhoneContactItem entity)
        {
            Label = entity.Label;
            Number = entity.PhoneNumber;
        }

        public void Mergue(PhoneContactItem entity)
        {
            entity.Label = Label;
            entity.PhoneNumber = Number;
        }
    }
}
