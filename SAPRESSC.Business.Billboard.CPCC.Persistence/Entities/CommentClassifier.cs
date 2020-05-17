using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    public class CommentClassifier : Core.Persistence.Entities.IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Phrase { get; set; }
    }
}
