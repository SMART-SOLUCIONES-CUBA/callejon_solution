using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Advertisement : Core.Persistence.Entities.IEntity<System.Guid>
    {
        [Key]
        public System.Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public Middleware.Media.Persistence.Entities.Image PlacardImage { get; set; }

        public string About { get; set; }

        [Url]
        public string Action { get; set; }
    }
}
