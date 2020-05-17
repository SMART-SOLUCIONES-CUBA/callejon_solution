using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CategoryInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Category, int>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public Category Export()
        {
            var entity = new Category();

            Mergue(entity);

            return entity;
        }

        public void Import(Category entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public void Mergue(Category entity)
        {
            entity.Name = Name;
        }
    }
}
