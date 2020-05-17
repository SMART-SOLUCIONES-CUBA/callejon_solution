using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.Api;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("cpccbillboard")]
    public class AdvisesController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Persistence.Entities.Advise, int> repository;

        public AdvisesController(IEntityRepository<Persistence.Entities.Advise, int> repository, Microsoft.Extensions.Localization.IStringLocalizer<AdvisesController> localizer, ILogger<AdvisesController> logger) : base(localizer, logger)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<AdviseApiModel> Get()
        {
            var entities = repository.FindBy(entity => entity.Enabled);

            var models = new List<AdviseApiModel>();

            foreach (var item in entities)
            {
                models.Add(Factories.Api.Models.AdviseApiModelFactory.BuildFor(item));
            }

            return models;
        }
    }
}
