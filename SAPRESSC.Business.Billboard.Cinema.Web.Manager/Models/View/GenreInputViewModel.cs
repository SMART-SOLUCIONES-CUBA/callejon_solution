using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class GenreInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Genre, int>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public Genre Export()
        {
            var entity = new Genre();

            Mergue(entity);

            return entity;
        }

        public void Import(Genre entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public void Mergue(Genre entity)
        {
            entity.Id = Id;
            entity.Name = Name;
        }
    }
}