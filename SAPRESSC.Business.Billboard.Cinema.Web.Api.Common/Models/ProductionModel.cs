using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class ProductionModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string LocalizedTitle { get; set; }

        public System.TimeSpan Duration { get; set; }

        public int Year { get; set; }

        public string About { get; set; }

        public string Argument { get; set; }

        public ImageModel PosterImage { get; set; }

        public VideoModel TrailerVideo { get; set; }

        public LanguageModel Language { get; set; }

        public ClasificationModel Clasification { get; set; }

        public IEnumerable<GenreModel> Genres { get; set; }

        public CountryModel Country { get; set; }

        public CountryModel[] CoCountries { get; set; }

        public PersonalityModel[] Directors { get; set; }

        public PersonalityModel[] Interpreters { get; set; }

        public AwardWinningModel[] AwardWinnings { get; set; }

        public int Rate { get; set; }

        public ProductionModel()
        {
            Rate = 4;
        }
    }
}
