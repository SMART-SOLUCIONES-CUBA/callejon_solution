using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class ProducerHousesController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Core.Persistence.Repositories.IEntityRepository<Persistence.Entities.ProducerHouse, long> repository;

        public ProducerHousesController(Core.Persistence.Repositories.IEntityRepository<Persistence.Entities.ProducerHouse, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<ProducerHousesController> localizer, ILogger<ProducerHousesController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(repository.GetAll().Select(entity => new Web.Api.Common.Models.ProducerHouseModel
            {
                Id = entity.Id,
                Name = entity.Name,
                BornDate = entity.BornDate,
                About = entity.About,
                Image = new Web.Api.Common.Models.ImageModel { Id = entity.Image.Id, ContentType = entity.Image.ContentType }
            }).ToArray());
        }

        [HttpGet("relevant")]
        [HttpGet("relevant/{limit}")]
        public virtual IActionResult GetRelevant(int limit = 10)
        {
            return Ok(repository.GetAll().Select(entity => new Web.Api.Common.Models.ProducerHouseModel
            {
                Id = entity.Id,
                Name = entity.Name,
                BornDate = entity.BornDate,
                About = entity.About,
                Image = new Web.Api.Common.Models.ImageModel { Id = entity.Image.Id, ContentType = entity.Image.ContentType }
            }).Take(limit).ToArray());
        }
    }
}
