using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class PersonalityInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Personality, long>
    {
        public PersonalityInputViewModel()
        {
            BornDate = System.DateTime.Today.AddYears(-30);
        }

        public long Id { get; set; }

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

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid ImageId { get; set; }

        public bool ResetImage { get; set; }

        public Personality Export()
        {
            var entity = new Personality();

            Mergue(entity);

            return entity;
        }

        public void Import(Personality entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            LastName = entity.LastName;
            Sex = entity.Sex;
            BornDate = entity.BornDate;
            About = entity.About;
            ImageId = entity.Image != null ? entity.Image.Id : System.Guid.Empty;
        }

        public void Mergue(Personality entity)
        {
            entity.Name = Name;
            entity.LastName = LastName;
            entity.Sex = Sex;
            entity.BornDate = BornDate;
            entity.About = About;
        }
    }
}
