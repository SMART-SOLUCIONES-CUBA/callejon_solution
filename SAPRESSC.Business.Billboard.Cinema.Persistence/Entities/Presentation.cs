using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Presentation : Core.Persistence.Entities.IHabilitableEntity<long>
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public System.DateTime Date { get; set; }

        [Required]
        public Production Production { get; set; }

        [Required]
        public Room Room { get; set; }

        public bool Enabled { get; set; }
    }
}
