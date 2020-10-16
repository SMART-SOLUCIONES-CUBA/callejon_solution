using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class SearchesController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Cinema.Persistence.Entities.Search, long> searchRepository;

        public SearchesController(IEntityRepository<Cinema.Persistence.Entities.Search, long> searchRepository, Microsoft.Extensions.Localization.IStringLocalizer<SearchesController> localizer, ILogger<SearchesController> logger) : base(localizer, logger)
        {
            this.searchRepository = searchRepository;
        }

        [HttpGet("lastforsubscriptor/{key}")]
        [HttpGet("lastforsubscriptor/{key}/take/{limit}")]
        public virtual IActionResult GetLastForSubscriptor(string key, int limit = 10)
        {
            return Ok(searchRepository.FindBy(e => e.SubscriptionId == key).Select(e => EntityToModel(e)).OrderByDescending(e => e.Id).Take(limit).ToArray());
        }

        [HttpGet("lastotherforsubscriptor/{key}")]
        [HttpGet("lastotherforsubscriptor/{key}/take/{limit}")]
        public virtual IActionResult GetLastOtherForSubscriptor(string key, int limit = 10)
        {
            return Ok(searchRepository.FindBy(e => e.SubscriptionId != key).Select(e => EntityToModel(e)).OrderByDescending(e => e.Id).Take(limit).ToArray());
        }

        private Cinema.Web.Api.Common.Models.SearchModel EntityToModel(Cinema.Persistence.Entities.Search entity)
        {
            return new Cinema.Web.Api.Common.Models.SearchModel
            {
                Id = entity.Id,
                TitleTemathic = entity.TitleTemathic,
                Year = entity.Year,
                SubscriptionId = entity.SubscriptionId,
                Category = entity.Category == null ? null : new Cinema.Web.Api.Common.Models.CategoryModel { Id = entity.Category.Id, Name = entity.Category.Name },
                Country = entity.Country == null ? null : new Cinema.Web.Api.Common.Models.CountryModel { Id = entity.Country.Id, Name = entity.Country.Name, IsoCode = entity.Country.IsoCode },
                ProducerHouse = entity.ProducerHouse == null ? null : new Cinema.Web.Api.Common.Models.ProducerHouseModel
                {
                    Id = entity.ProducerHouse.Id,
                    Name = entity.ProducerHouse.Name,
                    BornDate = entity.ProducerHouse.BornDate,
                    About = entity.ProducerHouse.About,
                    Image = new Cinema.Web.Api.Common.Models.ImageModel { Id = entity.ProducerHouse.Image.Id, ContentType = entity.ProducerHouse.Image.ContentType }
                },
                Genres = entity.GenreBridges.Select(bridge => new Cinema.Web.Api.Common.Models.GenreModel { Id = bridge.GenreId, Name = bridge.Genre.Name }).ToArray(),
                Personalities = entity.PersonalityBridges.Select(bridge => new Cinema.Web.Api.Common.Models.PersonalityModel
                {
                    Id = bridge.PersonalityId,
                    Name = bridge.Personality.Name,
                    LastName = bridge.Personality.LastName,
                    BornDate = bridge.Personality.BornDate,
                    IsMale = bridge.Personality.Sex == Middleware.Identity.Persistence.Enumerations.Sex.Male,
                    About = bridge.Personality.About,
                    Image = bridge.Personality.Image == null ? null : new Cinema.Web.Api.Common.Models.ImageModel { Id = bridge.Personality.Image.Id, ContentType = bridge.Personality.Image.ContentType }
                }).ToArray(),
                Tags = entity.TagBridges.Select(bridge => new Cinema.Web.Api.Common.Models.TagModel { Id = bridge.TagId, Name = bridge.Tag.Name }).ToArray()
            };
        }
    }
}
