using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    public class PortableDbDownload : Core.Persistence.Entities.IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string UserAgent { get; set; }

        public System.DateTime Created { get; set; }

        public PortableDbDownload()
        {
            Created = System.DateTime.Now;
        }
    }
}
