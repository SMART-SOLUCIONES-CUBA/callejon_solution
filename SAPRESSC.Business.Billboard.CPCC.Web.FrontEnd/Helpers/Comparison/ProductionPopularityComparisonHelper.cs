using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Helpers.Comparison
{
    public class ProductionPopularityComparisonHelper : System.Collections.Generic.IComparer<Production>
    {
        public int Compare(Production target, Production reference)
        {
            var targetIndex = (target.Checkouts.Count * 2) + (target.FetchOrders.Count * 3);

            var referenceIndex = (reference.Checkouts.Count * 2) + (reference.FetchOrders.Count * 3);

            return targetIndex - referenceIndex;
        }
    }
}
