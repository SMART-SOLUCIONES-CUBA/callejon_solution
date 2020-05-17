using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class CommentClassifierDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<CommentClassifier, int>
    {
        public int Id { get; set; }

        public string Phrase { get; set; }

        public void Import(CommentClassifier entity)
        {
            Id = entity.Id;
            Phrase = entity.Phrase;
        }
    }
}
