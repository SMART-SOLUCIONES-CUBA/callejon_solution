using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class LanguageInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Language, int>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }


        public Language Export()
        {
            var entity = new Language();

            Mergue(entity);

            return entity;

        }

        public void Import(Language entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public void Mergue(Language entity)
        {
            entity.Id = Id;
            entity.Name = Name;
        }
    }
}
