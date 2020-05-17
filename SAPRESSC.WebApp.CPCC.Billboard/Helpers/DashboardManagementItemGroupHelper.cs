using SmartSolucionesCuba.SAPRESSC.Core.Web.Management;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Helpers
{
    internal static class DashboardManagementItemGroupHelper
    {
        internal static Dictionary<string, ICollection<Core.Web.Management.Models.View.DashboardManageItemViewModel>> GetGrouped(Core.Web.Common.IApplicationContext applicationContext)
        {
            var modules = applicationContext.GetWebModules();

            var securityItems = new List<Core.Web.Management.Models.View.DashboardManageItemViewModel>();
            var billboardItems = new List<Core.Web.Management.Models.View.DashboardManageItemViewModel>();
            var supportItems = new List<Core.Web.Management.Models.View.DashboardManageItemViewModel>();

            foreach (var module in modules)
            {
                if (module is IManagementApplicationModule)
                {

                    if (module is Core.Web.SecurityLocal.LocalSecurityWebModule)
                    {
                        securityItems = (module as IManagementApplicationModule).GetDashboardManagementItems().ToList();
                    }
                    else if (module is Business.Billboard.Cinema.Web.Manager.CinemaBillboardBusinessManagementApplicationModule)
                    {
                        var rawItems = (module as IManagementApplicationModule).GetDashboardManagementItems();

                        var publicsControlls = new string[] { "advises", "events", "productions", "presentations", "fixscreenclientconfigurations" };
                        var supportControlls = new string[] { "genres", "awards", "languages", "cinemas", "producerhouses", "personalities", "categories" };

                        billboardItems.AddRange(rawItems.Where(item => publicsControlls.Contains(item.Controller)).ToArray());
                        supportItems.AddRange(rawItems.Where(item => supportControlls.Contains(item.Controller)).ToArray());
                    }
                    else if (module is Middleware.Region.Web.Manager.RegionMiddlewareManagementApplicationModule)
                    {
                        supportItems.Add((module as IManagementApplicationModule).GetDashboardManagementItems().First(item => item.Controller == "countries"));
                    }
                    else if (module is Business.Billboard.CPCC.Web.BackEnd.CPCCBillboardBusinessManagementApplicationModule)
                    {
                        var rawItems = (module as IManagementApplicationModule).GetDashboardManagementItems();

                        billboardItems.AddRange(rawItems.Where(item => item.Controller != "commentclassifiers").ToArray());
                        supportItems.Add(rawItems.First(item => item.Controller == "commentclassifiers"));
                    }
                }
            }

            var result = new Dictionary<string, ICollection<Core.Web.Management.Models.View.DashboardManageItemViewModel>> { };

            result.Add("Seguridad", securityItems);
            result.Add("Cartelera", billboardItems);
            result.Add("Complementarios", supportItems);

            return result;
        }
    }
}
