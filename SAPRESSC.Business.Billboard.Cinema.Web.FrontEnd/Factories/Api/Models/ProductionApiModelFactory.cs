using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class ProductionApiModelFactory
    {
        public static ProductionApiModel BuildFor(Persistence.Entities.Production production, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var interpretationParticipations = production.Participations.Where(entity => entity.Role == ProductionRole.Interpretation);

            var model = new ProductionApiModel
            {
                Id = production.Id,
                Title = production.Title,
                LocalizedTitle = production.LocalizedTitle,
                Language = new LanguageApiModel { Id = production.Language.Id, Name = production.Language.Name },
                Clasification = new ClasificationApiModel { Id = production.Clasification.Id, Name = production.Clasification.Name },
                Country = new CountryApiModel { Id = production.Country.Id, Name = production.Country.Name, IsoCode = production.Country.IsoCode },
                CoCountries = production.CountryCoProductionBridges.Select(bridge => new CountryApiModel { Id = bridge.Country.Id, Name = bridge.Country.Name, IsoCode = bridge.Country.IsoCode }).ToArray(),
                Rate = production.Rate,
                Duration = System.TimeSpan.FromMinutes(production.Duration),
                Year = production.Year,
                About = production.About,
                Argument = production.Argument,
                PosterImage = ImageApiModelFactory.BuildFor(production.PosterImage, mediaFileManager, cached),
                Genres = production.ProductionGenreBridges.Select(bridge => new GenreApiModel { Id = bridge.Genre.Id, Name = bridge.Genre.Name }),
                Directors = production.Participations.Where(entity => entity.Role == ProductionRole.Direction).Select(entity => PersonalityApiModelFactory.BuildFor(entity.Personality, mediaFileManager, cached)).ToArray(),
                Interpreters = production.Participations.Where(entity => entity.Role == ProductionRole.Interpretation).Select(entity => PersonalityApiModelFactory.BuildFor(entity.Personality, mediaFileManager, cached)).ToArray(),
                AwardWinnings = production.AwardWinnings.Select(entity => new AwardWinningApiModel { Id = entity.Id, Type = (int)entity.Type, Edition = entity.Edition, Category = entity.Category, Award = new AwardApiModel { Id = entity.Award.Id, Name = entity.Award.Name } }).ToArray() //string.Format("{0} Premios {1} {2}, {3}", entity.Type == AwardWinType.Winner ? "Ganador" : "Nominado", entity.Award.Name, entity.Edition, entity.Category)).ToArray()
            };


            if (production.TrailerVideo != null)
            {
                model.TrailerVideo = VideoApiModelFactory.BuildFor(production.TrailerVideo, mediaFileManager, cached);
            }

            return model;
        }
    }
}
