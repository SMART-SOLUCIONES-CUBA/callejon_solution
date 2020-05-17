using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Review : Core.Persistence.Entities.IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public System.DateTime Published { get; set; }

        [Required]
        public Personality Author { get; set; }

        [Required]
        public Production Production { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
