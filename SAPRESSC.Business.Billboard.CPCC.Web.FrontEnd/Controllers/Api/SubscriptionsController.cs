using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Common.Extensions;
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

        [HttpGet("{id}/{password}")]
        public virtual IActionResult Get(string id, string password)
        {
            try
            {
                var entity = repository.Get(id);

                if (!entity.Password.Equals(password.ToHashedPassword())) return Unauthorized();

                if (!entity.Enabled) return Forbid();

                var result = new Cinema.Web.Api.Common.Models.SubscriptionModel
                {
                    Id = entity.Id,
                    Name = entity.Identity.Name,
                    LastName = entity.Identity.LastName,
                    About = entity.Identity.About,
                    ImageId = entity.Identity.Image?.Id ?? System.Guid.Empty,
                    PasswordChangeRequired = entity.PasswordChangeRequired,
                    LastSignInAt = entity.LastSignInAt,
                    ServiceAvailabilityFrom = entity.ServiceAvailabilityFrom,
                    ServiceAvailabilityTo = entity.ServiceAvailabilityTo,
                    PreferedGenres = entity.PreferedGenreBridges.Select(bridge => new Cinema.Web.Api.Common.Models.GenreModel
                    {
                        Id = bridge.Genre.Id,
                        Name = bridge.Genre.Name
                    }).ToArray(),
                    PreferedTags = entity.PreferedTagBridges.Select(bridge => new Cinema.Web.Api.Common.Models.TagModel
                    {
                        Id = bridge.Tag.Id,
                        Name = bridge.Tag.Name
                    }).ToArray(),
                    PreferedProducerHouses = entity.PreferedProducerHouseBridges.Select(bridge => new Cinema.Web.Api.Common.Models.ProducerHouseModel
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
                };

                entity.LastSignInAt = System.DateTime.Now;

                repository.Update(entity, entity.Id);

                return Ok(result);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        public virtual IActionResult Update([FromBody] Cinema.Web.Api.Common.Models.SubscriptionModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = repository.Get(model.Id);

                    entity.PreferedGenreBridges = model.PreferedGenres.Select(item => new Cinema.Persistence.Entities.SubscriptionGenreBridge
                    {
                        SubscriptionId = model.Id,
                        GenreId = item.Id
                    }).ToList();
                    entity.PreferedTagBridges = model.PreferedTags.Select(item => new Cinema.Persistence.Entities.SubscriptionTagBridge
                    {
                        SubscriptionId = model.Id,
                        TagId = item.Id
                    }).ToList();
                    entity.PreferedProducerHouseBridges = model.PreferedProducerHouses.Select(item => new Cinema.Persistence.Entities.SubscriptionProducerHouseBridge
                    {
                        SubscriptionId = model.Id,
                        ProducerHouseId = item.Id
                    }).ToList();

                    repository.Update(entity, User.Identity.Name);

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
