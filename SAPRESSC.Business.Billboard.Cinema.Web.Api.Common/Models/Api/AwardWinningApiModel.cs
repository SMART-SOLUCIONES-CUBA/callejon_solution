namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api
{
    public class AwardWinningApiModel
    {
        public int Id { get; set; }

        public string Edition { get; set; }

        public int Type { get; set; }

        public AwardApiModel Award { get; set; }

        public string Category { get; set; }
    }
}
