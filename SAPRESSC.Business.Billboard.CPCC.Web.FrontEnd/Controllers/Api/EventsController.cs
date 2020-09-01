using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Facades.Manager;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("cpccbillboard")]
    public class EventsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Persistence.Entities.Event, System.Guid> repository;
        private readonly IMediaFileManagerFacade mediaFileManagerFacade;

        public EventsController(IMediaFileManagerFacade mediaFileManagerFacade, IEntityRepository<Persistence.Entities.Event, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<EventsController> localizer, ILogger<EventsController> logger) : base(localizer, logger)
        {
            this.repository = repository;
            this.mediaFileManagerFacade = mediaFileManagerFacade;
        }

        [HttpGet]
        [HttpGet("cached/{cached}")]
        public IEnumerable<EventModel> Get(bool cached = false)
        {
            var startEdgeDate = System.DateTime.Today;

            System.Linq.Expressions.Expression<System.Func<Persistence.Entities.Event, bool>> criteria = entity => ((startEdgeDate <= entity.Start.Date) || (startEdgeDate <= entity.End.Date));

            var publicsOnly = !Request.Headers.Contains(new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("x-client", new Microsoft.Extensions.Primitives.StringValues("SAPRESSC.WpfApp.CPCC.Billboard.ReferenceClient")));

            if (publicsOnly)
            {
                criteria = entity => entity.Enabled && ((startEdgeDate <= entity.Start.Date) || (startEdgeDate <= entity.End.Date));
            }

            var entities = repository.FindBy(criteria).OrderBy(entity => entity.Start);

            var models = new List<EventModel>();

            foreach (var item in entities)
            {
                models.Add(Factories.Api.Models.EventModelFactory.BuildFor(item, mediaFileManagerFacade, cached));
            }

            return models;
        }
    }
}
