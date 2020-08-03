using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cinemabillboard")]
    public class CategoriesController : Core.Web.Management.Controllers.AbstractEntityManagementController<Category, System.Guid, CategoryInputViewModel, CategoryDisplayViewModel>
    {
        public CategoriesController(IEntityRepository<Category, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CategoriesController> localizer, ILogger<CategoriesController> logger) : base(repository, localizer, logger)
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
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerEditor)]
        public override IActionResult Edit(CategoryInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerRemover)]
        public override IActionResult Delete(CategoryDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override CategoryInputViewModel ConfigureCreate(CategoryInputViewModel modelInput)
        {
            var result = base.ConfigureCreate(modelInput);

            PopulateModelInputForAvailableParents(modelInput);

            return result;
        }

        protected override CategoryInputViewModel ConfigureEdit(Category entity, CategoryInputViewModel modelInput)
        {
            var result = base.ConfigureEdit(entity, modelInput);

            PopulateModelInputForAvailableParents(modelInput);

            return result;
        }

        protected override CategoryInputViewModel ConfigurePostFailCreateValidation(CategoryInputViewModel modelInput)
        {
            var result = base.ConfigurePostFailCreateValidation(modelInput);

            PopulateModelInputForAvailableParents(modelInput);

            return result;
        }

        protected override CategoryInputViewModel ConfigurePostFailEditValidation(CategoryInputViewModel modelInput)
        {
            var result = base.ConfigurePostFailEditValidation(modelInput);

            PopulateModelInputForAvailableParents(modelInput);

            return result;
        }

        private void PopulateModelInputForAvailableParents(CategoryInputViewModel inputModel)
        {
            inputModel.AvailableCategorySelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = System.Guid.Empty.ToString(),
                Text = "Ninguna"
            });

            var entities = Repository.GetAll().ToList().Where(e => !e.GetTreeId().Contains(inputModel.Id.ToString())).OrderBy(e => e.GetTreeId()).ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableCategorySelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.GetTreeName() });
            }
        }
    }
}
