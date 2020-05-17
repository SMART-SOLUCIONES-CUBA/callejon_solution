using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    public class Advise : Core.Persistence.Entities.IHabilitableEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        public bool Enabled { get; set; }
    }
}
