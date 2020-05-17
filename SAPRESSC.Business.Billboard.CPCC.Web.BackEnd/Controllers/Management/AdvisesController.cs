using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.Filter;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cpccbillboard")]
    public class AdvisesController : Core.Web.Management.Controllers.AbstractHabilitableEntityManagementFilteredController<Advise, int, AdviseInputViewModel, AdviseDisplayViewModel, AdviseFilterModel>
    {
        public AdvisesController(IEntityRepository<Advise, int> repository, Microsoft.Extensions.Localization.IStringLocalizer<AdvisesController> localizer, ILogger<AdvisesController> logger) : base(repository, localizer, logger)
        {
        }

        protected override string GetFilterSessionID()
        {
            return "cpccbillboard-advises-management-filter";
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerViewer)]
        public override IActionResult Filter(AdviseFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerCreator)]
        public override IActionResult Create(AdviseInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerEditor)]
        public override IActionResult Edit(int key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerEditor)]
        public override IActionResult Edit(AdviseInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerEditor)]
        public override ActionResult Enable(int key)
        {
            return base.Enable(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerEditor)]
        public override ActionResult Disable(int key)
        {
            return base.Disable(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerRemover)]
        public override IActionResult Delete(int key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerRemover)]
        public override IActionResult Delete(AdviseDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
