using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Supporter.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Supporter.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class DriveInfoController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        [Microsoft.AspNetCore.Mvc.HttpGet("{key}")]
        public Api.Common.Models.DriveInfoModel Get(string key)
        {
            // TODO Allow only local host requests!!

            var driveInfo = new System.IO.DriveInfo(key);

            return driveInfo.ToApiModel();
        }
    }
}
