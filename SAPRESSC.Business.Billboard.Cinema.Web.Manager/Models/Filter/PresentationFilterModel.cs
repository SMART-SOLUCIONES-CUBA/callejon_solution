namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.Filter
{
    public class PresentationFilterModel : Core.Web.Management.Models.Filter.IEntityFilterModel<Persistence.Entities.Presentation, long>
    {
        public string ProductionTitle { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Persistence.Entities.Presentation, bool>> GetSelector()
        {
            if (ProductionTitle == null || ProductionTitle == string.Empty) return null;

            return entity => entity.Production.Title.Contains(ProductionTitle) || entity.Production.LocalizedTitle.Contains(ProductionTitle);
        }
    }
}
