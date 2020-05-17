using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class RoomInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Room, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public long CinemaId { get; set; }

        public Room Export()
        {
            var entity = new Room();

            Mergue(entity);

            return entity;
        }

        public void Import(Room entity)
        {
            Name = entity.Name;
        }

        public void Mergue(Room entity)
        {
            entity.Name = Name;
        }
    }
}
