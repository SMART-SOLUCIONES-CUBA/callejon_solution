using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class TagsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Core.Persistence.Repositories.IEntityRepository<Persistence.Entities.Tag, int> repository;

        public TagsController(Core.Persistence.Repositories.IEntityRepository<Persistence.Entities.Tag, int> repository, Microsoft.Extensions.Localization.IStringLocalizer<TagsController> localizer, ILogger<TagsController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(repository.GetAll().Select(entity => new Web.Api.Common.Models.TagModel
            {
                Id = entity.Id,
                Name = entity.Name
            }).ToArray());
        }
    }
}