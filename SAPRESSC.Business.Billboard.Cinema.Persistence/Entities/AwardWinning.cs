using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class AwardWinning : Core.Persistence.Entities.IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Edition { get; set; }

        [Required]
        public Enumerations.AwardWinType Type { get; set; }

        [Required]
        public Award Award { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public Production Production { get; set; }
    }
}
