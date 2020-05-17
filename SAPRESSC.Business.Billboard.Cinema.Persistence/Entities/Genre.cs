using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Genre : Core.Persistence.Entities.INamedEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<ProductionGenreBridge> ProductionGenreBridges { get; set; }

        public Genre()
        {
            ProductionGenreBridges = new List<ProductionGenreBridge>();
        }
    }
}
