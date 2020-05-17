using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class CommentClassifierInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<CommentClassifier, int>
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Frase")]
        public string Phrase { get; set; }

        public CommentClassifier Export()
        {
            var entity = new CommentClassifier();

            Mergue(entity);

            return entity;
        }

        public void Import(CommentClassifier entity)
        {
            Id = entity.Id;
            Phrase = entity.Phrase;
        }

        public void Mergue(CommentClassifier entity)
        {
            entity.Phrase = Phrase;
        }
    }
}
