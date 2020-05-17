using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api
{
    public class ProductionApiModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string LocalizedTitle { get; set; }

        public System.TimeSpan Duration { get; set; }

        public int Year { get; set; }

        public string About { get; set; }

        public string Argument { get; set; }

        public ImageApiModel PosterImage { get; set; }

        public VideoApiModel TrailerVideo { get; set; }

        public LanguageApiModel Language { get; set; }

        public CategoryApiModel Category { get; set; }

        public IEnumerable<GenreApiModel> Genres { get; set; }

        public CountryApiModel Country { get; set; }

        public CountryApiModel[] CoCountries { get; set; }

        public PersonalityApiModel[] Directors { get; set; }

        public PersonalityApiModel[] Interpreters { get; set; }

        public AwardWinningApiModel[] AwardWinnings { get; set; }

        public int Rate { get; set; }

        public ProductionApiModel()
        {
            Rate = 4;
        }
    }
}
