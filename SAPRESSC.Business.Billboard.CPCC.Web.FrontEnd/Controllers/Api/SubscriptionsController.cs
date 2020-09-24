using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class SubscriptionsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Core.Persistence.Repositories.IEntityRepository<Cinema.Persistence.Entities.Subscription, string> repository;

        public SubscriptionsController(Core.Persistence.Repositories.IEntityRepository<Cinema.Persistence.Entities.Subscription, string> repository, Microsoft.Extensions.Localization.IStringLocalizer<SubscriptionsController> localizer, ILogger<SubscriptionsController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet("{key}")]
        public virtual IActionResult Get(string key)
        {
            var target = repository.FindOneBy(entity => entity.Id == key);

            if (target == null) return NoContent();

            return Ok(new Cinema.Web.Api.Common.Models.SubscriptionModel
            {
                Id = target.Id,
                PreferedGenres = target.PreferedGenreBridges.Select(bridge => new Cinema.Web.Api.Common.Models.GenreModel
                {
                    Id = bridge.Genre.Id,
                    Name = bridge.Genre.Name
                }).ToArray(),
                PreferedTags = target.PreferedTagBridges.Select(bridge => new Cinema.Web.Api.Common.Models.TagModel
                {
                    Id = bridge.Tag.Id,
                    Name = bridge.Tag.Name
                }).ToArray(),
                PreferedProducerHouses = target.PreferedProducerHouseBridges.Select(bridge => new Cinema.Web.Api.Common.Models.ProducerHouseModel
                {
                    Id = bridge.ProducerHouse.Id,
                    Name = bridge.ProducerHouse.Name,
                    BornDate = bridge.ProducerHouse.BornDate,
                    About = bridge.ProducerHouse.About,
                    Image = new Cinema.Web.Api.Common.Models.ImageModel
                    {
                        Id = bridge.ProducerHouse.Image.Id,
                        ContentType = bridge.ProducerHouse.Image.ContentType
                    }
                }).ToArray()
            });
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] Cinema.Web.Api.Common.Models.SubscriptionModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new Cinema.Persistence.Entities.Subscription
                    {
                        Id = model.Id,
                        PreferedGenreBridges = model.PreferedGenres.Select(item => new Cinema.Persistence.Entities.SubscriptionGenreBridge
                        {
                            SubscriptionId = model.Id,
                            GenreId = item.Id
                        }).ToList(),
                        PreferedTagBridges = model.PreferedTags.Select(item => new Cinema.Persistence.Entities.SubscriptionTagBridge
                        {
                            SubscriptionId = model.Id,
                            TagId = item.Id
                        }).ToList(),
                        PreferedProducerHouseBridges = model.PreferedProducerHouses.Select(item => new Cinema.Persistence.Entities.SubscriptionProducerHouseBridge
                        {
                            SubscriptionId = model.Id,
                            ProducerHouseId = item.Id
                        }).ToList()
                    };

                    repository.Add(entity, User.Identity.Name);

                    return Ok();
                }
                catch (System.Exception e)
                {
                    logger.LogError(e.InnerException, e.Message);

                    return Conflict();
                }
            }

            return BadRequest();
        }
    }
}
