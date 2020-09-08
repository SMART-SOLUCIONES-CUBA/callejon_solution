namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class FilterResultPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private bool isSearchSummaryExpanded;
        private bool paginationHasNext;
        private int paginationCurrentPage;
        private System.Collections.ObjectModel.ObservableCollection<View.AudioVisualViewModel> results;
        private SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel search;
        private SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[] subCategories;

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel Search { get => search; set => SetProperty(ref search, value); }

        public System.Collections.ObjectModel.ObservableCollection<View.AudioVisualViewModel> Results { get => results; set => SetProperty(ref results, value); }

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[] SubCategories { get => subCategories; set => SetProperty(ref subCategories, value); }

        public bool IsSearchSummaryExpanded { get => isSearchSummaryExpanded; set => SetProperty(ref isSearchSummaryExpanded, value); }

        public bool PaginationHasNext { get => paginationHasNext; set => SetProperty(ref paginationHasNext, value); }

        public int PaginationCurrentPage { get => paginationCurrentPage; set => SetProperty(ref paginationCurrentPage, value); }
    }
}
