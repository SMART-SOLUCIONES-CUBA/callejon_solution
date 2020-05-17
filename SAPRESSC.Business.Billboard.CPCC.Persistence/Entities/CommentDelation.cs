using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class CommentDelation : Core.Persistence.Entities.IEntity<System.Guid>
    {
        public CommentDelation()
        {
            Id = System.Guid.NewGuid();
        }

        [Key]
        public System.Guid Id { get; set; }

        public System.DateTime Created { get; set; }

        [Required, MaxLength(255)]
        public string Note { get; set; }

        public Client Client { get; set; }

        public bool Revoked { get; set; }
    }
}
