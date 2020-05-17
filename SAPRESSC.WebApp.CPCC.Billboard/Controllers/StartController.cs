using Microsoft.AspNetCore.Mvc;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Controlles
{
    public class StartController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("management");
        }
    }
}
