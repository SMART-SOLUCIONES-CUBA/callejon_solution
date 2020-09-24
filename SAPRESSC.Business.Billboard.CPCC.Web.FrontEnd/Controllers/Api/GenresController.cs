using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class GenresController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Core.Persistence.Repositories.IEntityRepository<Cinema.Persistence.Entities.Genre, int> repository;

        public GenresController(Core.Persistence.Repositories.IEntityRepository<Cinema.Persistence.Entities.Genre, int> repository, Microsoft.Extensions.Localization.IStringLocalizer<GenresController> localizer, ILogger<GenresController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(repository.GetAll().Select(entity => new Cinema.Web.Api.Common.Models.GenreModel 
            {
                Id = entity.Id,
                Name = entity.Name
            }).ToArray());
        }

        [HttpGet("relevant")]
        [HttpGet("relevant/{limit}")]
        public virtual IActionResult GetRelevant(int limit = 10)
        {
            return Ok(repository.GetAll().Select(entity => new Cinema.Web.Api.Common.Models.GenreModel
            {
                Id = entity.Id,
                Name = entity.Name
            }).Take(limit).ToArray());
        }
    }
}
