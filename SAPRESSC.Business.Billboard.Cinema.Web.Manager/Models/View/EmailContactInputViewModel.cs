using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class EmailContactInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<EmailContactItem, long>
    {
        public long Id { get; set; }

        [Display(Name = "Label")]
        public string Label { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [EmailAddress(ErrorMessage = "EmailAddressValidationErrorMessage")]
        [Display(Name = "EmailAddress")]
        public string Address { get; set; }

        public long CinemaId { get; set; }

        public EmailContactItem Export()
        {
            var entity = new EmailContactItem();

            Mergue(entity);

            return entity;
        }

        public void Import(EmailContactItem entity)
        {
            Label = entity.Label;
            Address = entity.EmailAddress;
        }

        public void Mergue(EmailContactItem entity)
        {
            entity.Label = Label;
            entity.EmailAddress = Address;
        }
    }
}
