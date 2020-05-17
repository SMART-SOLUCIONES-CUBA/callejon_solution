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
    public class CommentClassifiersController : Core.Web.Management.Controllers.AbstractEntityManagementFilteredController<CommentClassifier, int, CommentClassifierInputViewModel, CommentClassifierDisplayViewModel, CommentClassifierFilterModel>
    {
        public CommentClassifiersController(IEntityRepository<CommentClassifier, int> repository, Microsoft.Extensions.Localization.IStringLocalizer<CommentClassifiersController> localizer, ILogger<CommentClassifiersController> logger) : base(repository, localizer, logger)
        {
        }

        protected override string GetFilterSessionID()
        {
            return "cpccbillboard-commentclassifiers-management-filter";
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerViewer)]
        public override IActionResult Filter(CommentClassifierFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerCreator)]
        public override IActionResult Create(CommentClassifierInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerEditor)]
        public override IActionResult Edit(int key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerEditor)]
        public override IActionResult Edit(CommentClassifierInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerRemover)]
        public override IActionResult Delete(int key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerRemover)]
        public override IActionResult Delete(CommentClassifierDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
