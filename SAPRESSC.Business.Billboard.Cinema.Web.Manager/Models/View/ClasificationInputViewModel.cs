using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class ClasificationInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Clasification, int>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public Clasification Export()
        {
            var entity = new Clasification();

            Mergue(entity);

            return entity;
        }

        public void Import(Clasification entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public void Mergue(Clasification entity)
        {
            entity.Name = Name;
        }
    }
}
