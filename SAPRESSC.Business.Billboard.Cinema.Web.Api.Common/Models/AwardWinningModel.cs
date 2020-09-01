namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class AwardWinningModel
    {
        public int Id { get; set; }

        public string Edition { get; set; }

        public int Type { get; set; }

        public AwardModel Award { get; set; }

        public string Category { get; set; }
    }
}
