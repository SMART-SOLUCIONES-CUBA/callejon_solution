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
    public class LanguagesController : Core.Web.Management.Controllers.AbstractEntityManagementController<Language, int, LanguageInputViewModel, LanguageDisplayViewModel>
    {
        public LanguagesController(IEntityRepository<Language, int> repository, Microsoft.Extensions.Localization.IStringLocalizer<LanguagesController> localizer, ILogger<LanguagesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerCreator)]
        public override IActionResult Create(LanguageInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerEditor)]
        public override IActionResult Edit(int key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerEditor)]
        public override IActionResult Edit(LanguageInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerRemover)]
        public override IActionResult Delete(int key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerRemover)]
        public override IActionResult Delete(LanguageDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
