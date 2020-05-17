using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Facades.Manager;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("cinemabillboard")]
    public class CinemaBillboardsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Persistence.Entities.Cinema, long> repository;
        private readonly IMediaFileManagerFacade mediaFileManagerFacade;

        public CinemaBillboardsController(IMediaFileManagerFacade mediaFileManagerFacade, IEntityRepository<Persistence.Entities.Cinema, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<CinemaBillboardsController> localizer, ILogger<CinemaBillboardsController> logger) : base(localizer, logger)
        {
            this.repository = repository;
            this.mediaFileManagerFacade = mediaFileManagerFacade;
        }

        [HttpGet]
        [HttpGet("cached/{cached}")]
        public IEnumerable<CinemaBillboardApiModel> Get(bool cached = false)
        {
            var entities = repository.GetAll();

            var publicsOnly = !Request.Headers.Contains(new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("x-client", new Microsoft.Extensions.Primitives.StringValues("SAPRESSC.WpfApp.CPCC.Billboard.ReferenceClient")));

            var models = new List<CinemaBillboardApiModel>();

            var startDate = System.DateTime.Today;
            var endDate = System.DateTime.Today.AddDays(7);

            if (Request.Query.ContainsKey("start") && Request.Query.ContainsKey("end"))
            {
                try
                {
                    startDate = new System.DateTime(long.Parse(Request.Query.First(pair => pair.Key == "start").Value[0]));
                    endDate = new System.DateTime(long.Parse(Request.Query.First(pair => pair.Key == "end").Value[0]));
                }
                catch { }
            }

            foreach (var item in entities)
            {
                var model = Factories.Api.Models.CinemaBillboardApiModelFactory.BuildFor(item, mediaFileManagerFacade, startDate, endDate, cached, publicsOnly);

                if (model.RoomBillboards.Count <= 0) continue;

                models.Add(model);
            }

            return models;
        }
    }
}
