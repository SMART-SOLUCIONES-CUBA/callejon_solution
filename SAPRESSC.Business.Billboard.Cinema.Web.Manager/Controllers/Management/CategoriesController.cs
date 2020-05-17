using Microsoft.AspNetCore.Mvc;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cinemabillboard")]
    public class CategoriesController : Core.Web.Management.Controllers.AbstractEntityManagementController<Category, int, CategoryInputViewModel, CategoryDisplayViewModel>
    {
        public CategoriesController(IEntityRepository<Category, int> repository, Microsoft.Extensions.Localization.IStringLocalizer<CategoriesController> localizer, Microsoft.Extensions.Logging.ILogger<CategoriesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerCreator)]
        public override IActionResult Create(CategoryInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerEditor)]
        public override IActionResult Edit(int key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerEditor)]
        public override IActionResult Edit(CategoryInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerRemover)]
        public override IActionResult Delete(int key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerRemover)]
        public override IActionResult Delete(CategoryDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
