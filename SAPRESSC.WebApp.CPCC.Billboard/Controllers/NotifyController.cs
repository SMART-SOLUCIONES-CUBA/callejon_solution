using Microsoft.AspNetCore.Mvc;
using SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Persistence.Context;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Controllers
{
    public class NotifyController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly ApplicationDbContext dbContext;

        public NotifyController(ApplicationDbContext dbContext, Microsoft.Extensions.Localization.IStringLocalizer<NotifyController> localizer, Microsoft.Extensions.Logging.ILogger<NotifyController> logger) : base(localizer, logger)
        {
            this.dbContext = dbContext;
        }

        [HttpPut]
        public IActionResult Update([FromBody] string client)
        {
            dbContext.PortableDbDownloads.Add(new Business.Billboard.CPCC.Persistence.Entities.PortableDbDownload { UserAgent = client });

            dbContext.SaveChanges("system");

            return Ok();
        }
    }
}
