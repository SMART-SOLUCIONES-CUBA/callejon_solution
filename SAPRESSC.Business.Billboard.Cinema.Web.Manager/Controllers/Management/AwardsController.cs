using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cinemabillboard")]
    public class AwardsController : Core.Web.Management.Controllers.AbstractEntityManagementController<Award, int, AwardInputViewModel, AwardDisplayViewModel>
    {
        public AwardsController(IEntityRepository<Award, int> repository, Microsoft.Extensions.Localization.IStringLocalizer<AwardsController> localizer, ILogger<AwardsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AwardManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AwardManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AwardManagerCreator)]
        public override IActionResult Create(AwardInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AwardManagerEditor)]
        public override IActionResult Edit(int key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AwardManagerEditor)]
        public override IActionResult Edit(AwardInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AwardManagerRemover)]
        public override IActionResult Delete(int key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AwardManagerRemover)]
        public override IActionResult Delete(AwardDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
