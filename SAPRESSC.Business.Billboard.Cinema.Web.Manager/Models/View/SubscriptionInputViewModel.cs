using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class SubscriptionInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Subscription, string>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        public string About { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 1048576, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public bool ResetImage { get; set; }

        public System.Guid ImageId { get; set; }

        public bool PasswordChangeRequired { get; set; }

        public bool Enabled { get; set; }

        public System.DateTime ServiceAvailabilityFrom { get; set; }

        public System.DateTime ServiceAvailabilityTo { get; set; }

        public virtual Subscription Export()
        {
            var entity = new Subscription
            {
                Id = Id
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Subscription entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            About = entity.Identity.About;
            ImageId = entity.Identity.Image?.Id ?? System.Guid.Empty;
            PasswordChangeRequired = entity.PasswordChangeRequired;
            Enabled = entity.Enabled;
            ServiceAvailabilityFrom = entity.ServiceAvailabilityFrom;
            ServiceAvailabilityTo = entity.ServiceAvailabilityTo;
        }

        public void Mergue(Subscription entity)
        {
            entity.Identity.Name = Name;
            entity.Identity.LastName = LastName;
            entity.Identity.About = About;
            entity.Enabled = Enabled;
            entity.PasswordChangeRequired = PasswordChangeRequired;
            entity.ServiceAvailabilityFrom = ServiceAvailabilityFrom;
            entity.ServiceAvailabilityTo = ServiceAvailabilityTo;
        }
    }
}
