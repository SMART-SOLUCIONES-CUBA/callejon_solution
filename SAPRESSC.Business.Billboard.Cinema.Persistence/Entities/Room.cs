using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Room : Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public Room()
        {
            Id = System.Guid.NewGuid();
            Presentations = new List<Presentation>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Cinema Cinema { get; set; }

        public ICollection<Presentation> Presentations { get; set; }
    }
}
