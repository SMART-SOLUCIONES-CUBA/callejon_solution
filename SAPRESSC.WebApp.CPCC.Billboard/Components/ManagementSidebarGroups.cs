using Microsoft.AspNetCore.Mvc;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Components
{
    public class ManagementSidebarGroups : ViewComponent
    {
        private readonly IApplicationContext applicationContext;

        public ManagementSidebarGroups(IApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public IViewComponentResult Invoke()
        {
            return View(Helpers.DashboardManagementItemGroupHelper.GetGrouped(applicationContext));
        }
    }
}
