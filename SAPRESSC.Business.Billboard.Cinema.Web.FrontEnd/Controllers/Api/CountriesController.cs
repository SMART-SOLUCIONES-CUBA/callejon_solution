using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class CountriesController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Core.Persistence.Repositories.IEntityRepository<Middleware.Region.Persistence.Entities.Country, int> repository;

        public CountriesController(Core.Persistence.Repositories.IEntityRepository<Middleware.Region.Persistence.Entities.Country, int> repository, Microsoft.Extensions.Localization.IStringLocalizer<CountriesController> localizer, ILogger<CountriesController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(repository.GetAll().Select(entity => new Web.Api.Common.Models.CountryModel
            {
                Id = entity.Id,
                Name = entity.Name
            }).ToArray());
        }
    }
}
