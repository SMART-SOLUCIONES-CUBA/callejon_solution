namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.Filter
{
    public class AdviseFilterModel : Core.Web.Management.Models.Filter.IEntityFilterModel<Persistence.Entities.Advise, int>
    {
        public string Message { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Persistence.Entities.Advise, bool>> GetSelector()
        {
            if (Message == null || Message == string.Empty) return null;

            return entity => entity.Message.Contains(Message);
        }
    }
}
