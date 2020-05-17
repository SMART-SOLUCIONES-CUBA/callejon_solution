using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Participation : Core.Persistence.Entities.IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public Enumerations.ProductionRole Role { get; set; }

        [Required]
        public virtual Personality Personality { get; set; }

        [Required]
        public Production Production { get; set; }

        public string Specification { get; set; }
    }
}
