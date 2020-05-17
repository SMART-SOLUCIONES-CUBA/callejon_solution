namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.Filter
{
    public class EventFilterModel : Core.Web.Management.Models.Filter.IEntityFilterModel<Persistence.Entities.Event, System.Guid>
    {
        public string Title { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Persistence.Entities.Event, bool>> GetSelector()
        {
            if (Title == null || Title == string.Empty) return null;

            return entity => entity.Title.Contains(Title);
        }
    }
}
