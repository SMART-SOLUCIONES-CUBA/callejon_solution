using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CinemaInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Persistence.Entities.Cinema, long>
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Longitude")]
        public double LocalizationLongitude { get; set; }

        [Display(Name = "Latitude")]
        public double LocalizationLatitude { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid ImageId { get; set; }

        public Persistence.Entities.Cinema Export()
        {
            var entity = new Persistence.Entities.Cinema();

            entity.SetPostalContactItems(new[] { new Middleware.Identity.Persistence.Entities.PostalContactItem() });

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Cinema entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Address = entity.GetPostalContactItems().First().Address;
            LocalizationLongitude = entity.LocalizationLongitude;
            LocalizationLatitude = entity.LocalizationLatitude;
            About = entity.About;
            ImageId = entity.Image != null ? entity.Image.Id : System.Guid.Empty;
        }

        public void Mergue(Persistence.Entities.Cinema entity)
        {
            entity.Name = Name;
            entity.GetPostalContactItems().First().Address = Address;
            entity.LocalizationLongitude = LocalizationLongitude;
            entity.LocalizationLatitude = LocalizationLatitude;
            entity.About = About;
        }
    }
}
