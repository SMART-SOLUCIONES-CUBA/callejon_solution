using System;
using System.Linq.Expressions;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.Filter
{
    public class CommentClassifierFilterModel : Core.Web.Management.Models.Filter.IEntityFilterModel<CommentClassifier, int>
    {
        public string Phrase { get; set; }

        public Expression<Func<CommentClassifier, bool>> GetSelector()
        {
            if (string.IsNullOrEmpty(Phrase)) return null;

            return entity => entity.Phrase.Contains(Phrase);
        }
    }
}
