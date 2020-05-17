using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Configurations;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Context;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Models.View;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.SecurityShared;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd
{
    public class CPCCBillboardBusinessManagementApplicationModule : Core.Web.Management.IManagementApplicationModule
    {
        public ICollection<DashboardManageItemViewModel> GetDashboardManagementItems()
        {
            return new List<DashboardManageItemViewModel>
            {
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cpccbillboard",
                    Controller = "advises",
                    Role = CPCCBillboardBusinessManagementRoleCodes.AdviseManagerViewer,
                    Title = "Avisos"
                },
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cpccbillboard",
                    Controller = "events",
                    Role = CPCCBillboardBusinessManagementRoleCodes.EventManagerViewer,
                    Title = "Eventos"
                },
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cpccbillboard",
                    Controller = "fixscreenclientconfigurations",
                    Role = CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerViewer,
                    Title = "Configuraciones"
                },
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cpccbillboard",
                    Controller = "productioncomments",
                    Role = CPCCBillboardBusinessManagementRoleCodes.ProductionCommentManagerViewer,
                    Title = "Comentarios"
                },
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cpccbillboard",
                    Controller = "commentclassifiers",
                    Role = CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerViewer,
                    Title = "Clasificadores"
                }
            };
        }

        public IDbContextDataInitialiser GetDbContextDataInitialiser()
        {
            return null;
        }

        public ICollection<IEntityTypeConfigurator> GetEntityTypeConfigurators()
        {
            return null;
        }

        public void RegisterServices(IServiceCollection services)
        {
        }
    }
}
