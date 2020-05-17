    using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class Comment : Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Comment()
        {
            Id = System.Guid.NewGuid();
            Banish = false;
            Created = System.DateTime.Now;
        }

        [Key]
        public System.Guid Id { get; set; }

        public System.DateTime Created { get; set; }

        [Required, StringLength(255)]
        public string Message { get; set; }

        public bool Banish { get; set; }

        public bool Syncronized { get; set; }

        public Client Client { get; set; }
    }
}
