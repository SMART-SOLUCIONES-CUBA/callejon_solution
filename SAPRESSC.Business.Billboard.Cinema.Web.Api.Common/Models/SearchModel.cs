namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class SearchModel
    {
        public long Id { get; set; }

        public string SubscriptionId { get; set; }

        public string TitleTemathic { get; set; }

        public CategoryModel Category { get; set; }

        public int Year { get; set; }

        public CountryModel Country { get; set; }

        public ProducerHouseModel ProducerHouse { get; set; }

        public GenreModel[] Genres { get; set; }

        public PersonalityModel[] Personalities { get; set; }

        public TagModel[] Tags { get; set; }
    }
}
