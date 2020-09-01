namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class PaginationModel<TModel>
    {
        public System.Collections.Generic.List<TModel> List { get; set; }

        public int Page { get; set; }

        public bool HasNext { get; set; }

        public bool HasPrevious { get; set; }

        public int MaxPageCount { get; set; }

        public int Limit { get; set; }
    }
}
