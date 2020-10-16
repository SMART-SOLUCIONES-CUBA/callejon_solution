using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Controllers.Api
{
    [Route("api/[area]/[controller]")]
    [Area("mediastore")]
    public class MediaStoreTerminalConfigurationsController : Core.Web.Common.Controllers.BaseWebController
    {
        private readonly Microsoft.Extensions.Options.IOptions<Options.MediaStoreOptions> mediaStoreOptions;

        public MediaStoreTerminalConfigurationsController(Microsoft.Extensions.Options.IOptions<Options.MediaStoreOptions> mediaStoreOptions, Microsoft.Extensions.Localization.IStringLocalizer<MediaStoreTerminalConfigurationsController> localizer, ILogger<MediaStoreTerminalConfigurationsController> logger) : base(localizer, logger)
        {
            this.mediaStoreOptions = mediaStoreOptions;
        }

        [HttpGet]
        public virtual IActionResult Get(string key)
        {
            return Ok(new Cinema.Web.Api.Common.Models.MediaStoreTerminalConfigurationModel
            {
                SharingUser = mediaStoreOptions.Value.SharingUser,
                SharingPassword = mediaStoreOptions.Value.SharingPassword,
                SharingRootFolder = mediaStoreOptions.Value.SharingRootFolder,
                SharingAudioVisualsFolder = mediaStoreOptions.Value.SharingAudioVisualsFolder
            });
        }
    }
}
