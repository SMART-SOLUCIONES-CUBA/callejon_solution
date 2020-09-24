using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class YearsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Core.Persistence.Repositories.IEntityRepository<Cinema.Persistence.Entities.Production, long> repository;

        public YearsController(Core.Persistence.Repositories.IEntityRepository<Cinema.Persistence.Entities.Production, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<YearsController> localizer, ILogger<YearsController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(repository.GetAll().GroupBy(entity => entity.Year).Select(group => group.Key).ToArray());
        }

        [HttpGet("relevant")]
        [HttpGet("relevant/{limit}")]
        public virtual IActionResult GetRelevant(int limit = 20)
        {
            return Ok(repository.GetAll().GroupBy(entity => entity.Year).Select(group => group.Key).Take(limit).ToArray());
        }
    }
}
