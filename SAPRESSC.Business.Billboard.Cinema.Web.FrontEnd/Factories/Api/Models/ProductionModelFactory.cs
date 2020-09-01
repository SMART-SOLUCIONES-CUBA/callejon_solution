using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class ProductionModelFactory
    {
        public static ProductionModel BuildFor(Persistence.Entities.Production production, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var interpretationParticipations = production.Participations.Where(entity => entity.Role == ProductionRole.Interpretation);

            var model = new ProductionModel
            {
                Id = production.Id,
                Title = production.Title,
                LocalizedTitle = production.LocalizedTitle,
                Language = new LanguageModel { Id = production.Language.Id, Name = production.Language.Name },
                Clasification = new ClasificationModel { Id = production.Clasification.Id, Name = production.Clasification.Name },
                Country = new CountryModel { Id = production.Country.Id, Name = production.Country.Name, IsoCode = production.Country.IsoCode },
                CoCountries = production.CountryCoProductionBridges.Select(bridge => new CountryModel { Id = bridge.Country.Id, Name = bridge.Country.Name, IsoCode = bridge.Country.IsoCode }).ToArray(),
                Rate = production.Rate,
                Duration = System.TimeSpan.FromMinutes(production.Duration),
                Year = production.Year,
                About = production.About,
                Argument = production.Argument,
                PosterImage = ImageModelFactory.BuildFor(production.PosterImage, mediaFileManager, cached),
                Genres = production.ProductionGenreBridges.Select(bridge => new GenreModel { Id = bridge.Genre.Id, Name = bridge.Genre.Name }),
                Directors = production.Participations.Where(entity => entity.Role == ProductionRole.Direction).Select(entity => PersonalityModelFactory.BuildFor(entity.Personality, mediaFileManager, cached)).ToArray(),
                Interpreters = production.Participations.Where(entity => entity.Role == ProductionRole.Interpretation).Select(entity => PersonalityModelFactory.BuildFor(entity.Personality, mediaFileManager, cached)).ToArray(),
                AwardWinnings = production.AwardWinnings.Select(entity => new AwardWinningModel { Id = entity.Id, Type = (int)entity.Type, Edition = entity.Edition, Category = entity.Category, Award = new AwardModel { Id = entity.Award.Id, Name = entity.Award.Name } }).ToArray() //string.Format("{0} Premios {1} {2}, {3}", entity.Type == AwardWinType.Winner ? "Ganador" : "Nominado", entity.Award.Name, entity.Edition, entity.Category)).ToArray()
            };


            if (production.TrailerVideo != null)
            {
                model.TrailerVideo = VideoModelFactory.BuildFor(production.TrailerVideo, mediaFileManager, cached);
            }

            return model;
        }
    }
}
