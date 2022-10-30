namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.Filter
{
    public class SubscriptionFilterModel : Core.Web.Management.Models.Filter.IEntityFilterModel<Persistence.Entities.Subscription, string>
    {
        public string FullName { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Persistence.Entities.Subscription, bool>> GetSelector()
        {
            if (FullName == null || FullName == string.Empty)
            {
                return null;
            }

            return entity => entity.Identity.Name.Contains(FullName) || entity.Identity.LastName.Contains(FullName);
        }
    }
}
