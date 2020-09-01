using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("cpccbillboard")]
    public class FixScreenClientConfigurationsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Persistence.Entities.FixScreenClientConfiguration, System.Guid> repository;

        public FixScreenClientConfigurationsController(IEntityRepository<Persistence.Entities.FixScreenClientConfiguration, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<FixScreenClientConfigurationsController> localizer, ILogger<FixScreenClientConfigurationsController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet("{key}")]
        public FixScreenClientConfigurationModel Get(System.Guid key)
        {
            var entity = repository.FindOneBy(e => e.Id == key && e.Enabled);

            if (entity == null)
            {
                entity = repository.Get(Common.Constants.DEFAULT_FIXSCREENCLIENT_CONFIGURATION_ID);
            }

            return Factories.Api.Models.FixScreenClientConfigurationModelFactory.BuildFor(entity);
        }
    }
}
