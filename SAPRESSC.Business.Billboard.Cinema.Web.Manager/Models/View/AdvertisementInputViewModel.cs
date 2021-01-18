using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class AdvertisementInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Persistence.Entities.Advertisement, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Action")]
        public string Action { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile PlacardImageFile { get; set; }

        public System.Guid PlacardImageId { get; set; }

        public Persistence.Entities.Advertisement Export()
        {
            var entity = new Persistence.Entities.Advertisement();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Advertisement entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Action = entity.Action;
            About = entity.About;
            PlacardImageId = entity.PlacardImage?.Id ?? System.Guid.Empty;
        }

        public void Mergue(Persistence.Entities.Advertisement entity)
        {
            entity.Title = Title;
            entity.Action = Action;
            entity.About = About;
        }
    }
}
