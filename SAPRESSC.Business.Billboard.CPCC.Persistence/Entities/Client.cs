using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    public class Client : Core.Persistence.Entities.IEntity<System.Guid>
    {
        [Key]
        public System.Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public System.DateTime BornDate { get; set; }

        [StringLength(255)]
        public string About { get; set; }

        public Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        public Middleware.Media.Persistence.Entities.Image Image { get; set; }
    }
}
