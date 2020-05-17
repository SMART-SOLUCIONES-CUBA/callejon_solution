using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class ProducerHouseInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<ProducerHouse, long>
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid ImageId { get; set; }

        public bool ResetImage { get; set; }

        public ProducerHouse Export()
        {
            var entity = new ProducerHouse();

            Mergue(entity);

            return entity;
        }

        public void Import(ProducerHouse entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            About = entity.About;
            ImageId = entity.Image != null ? entity.Image.Id : System.Guid.Empty;
        }

        public void Mergue(ProducerHouse entity)
        {
            entity.Name = Name;
            entity.About = About;
        }
    }
}
