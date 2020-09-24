namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class SubscriptionModel
    {
        public string Id { get; set; }

        public ProducerHouseModel[] PreferedProducerHouses { get; set; }

        public GenreModel[] PreferedGenres { get; set; }

        public TagModel[] PreferedTags { get; set; }
    }
}
