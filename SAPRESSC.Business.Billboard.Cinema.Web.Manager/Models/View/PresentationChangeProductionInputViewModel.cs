namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class PresentationChangeProductionInputViewModel : PresentationInputViewModel
    {
        public Core.Common.Models.View.PaginationViewModel<ProductionDisplayViewModel> ProductionsPagination { get; set; }
    }
}
