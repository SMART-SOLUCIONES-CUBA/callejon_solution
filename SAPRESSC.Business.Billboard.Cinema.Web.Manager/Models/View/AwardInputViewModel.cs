using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class AwardInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Award, int>
    {
        public int Id { get; set;}

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set;}

       
        public Award Export()
        {
            var entity = new Award();

            Mergue(entity);

            return entity;

        }

        public void Import(Award entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public void Mergue(Award entity)
        {
            entity.Id = Id;
            entity.Name = Name;
        }
    }
}
