namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class FilterResultPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private bool isSearchSummaryExpanded;
        private bool paginationHasNext;
        private int paginationCurrentPage;
        private System.Collections.ObjectModel.ObservableCollection<View.AudioVisualViewModel> results;

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel Search { get; set; }

        public System.Collections.ObjectModel.ObservableCollection<View.AudioVisualViewModel> Results { get => results; set => SetProperty(ref results, value); }

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[] SubCategories { get; set; }

        public bool IsSearchSummaryExpanded { get => isSearchSummaryExpanded; set => SetProperty(ref isSearchSummaryExpanded, value); }

        public bool PaginationHasNext { get => paginationHasNext; set => SetProperty(ref paginationHasNext, value); }

        public int PaginationCurrentPage { get => paginationCurrentPage; set => SetProperty(ref paginationCurrentPage, value); }
    }
}
