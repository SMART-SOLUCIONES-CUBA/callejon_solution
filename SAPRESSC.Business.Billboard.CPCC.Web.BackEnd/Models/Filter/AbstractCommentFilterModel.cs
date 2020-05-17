namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.Filter
{
    public abstract class AbstractCommentFilterModel<TComment> : Core.Web.Management.Models.Filter.IEntityFilterModel<TComment, System.Guid>
        where TComment : Persistence.Entities.Comment
    {
        public string Message { get; set; }

        public virtual System.Linq.Expressions.Expression<System.Func<TComment, bool>> GetSelector()
        {
            if (Message == null || Message == string.Empty) return null;

            return entity => entity.Message.Contains(Message);
        }
    }
}
