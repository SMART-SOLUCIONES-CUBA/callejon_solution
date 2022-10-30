using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.Filter;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("mediastore")]
    public class SubscriptionsController : Core.Web.Management.Controllers.AbstractHabilitableEntityManagementFilteredController<Subscription, string, SubscriptionInputViewModel, SubscriptionDisplayViewModel, SubscriptionFilterModel>
    {
        private readonly IEntityRepository<Middleware.Media.Persistence.Entities.Image, System.Guid> imageRepository;
        private readonly IEntityRepository<Middleware.Identity.Persistence.Entities.Person, long> identityRepository;
        private readonly Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager;

        public SubscriptionsController(Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, IEntityRepository<Middleware.Identity.Persistence.Entities.Person, long> identityRepository, IEntityRepository<Middleware.Media.Persistence.Entities.Image, System.Guid> imageRepository, IEntityRepository<Subscription, string> repository, Microsoft.Extensions.Localization.IStringLocalizer<SubscriptionsController> localizer, ILogger<SubscriptionsController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.identityRepository = identityRepository;
            this.mediaFileManager = mediaFileManager;
        }

        protected override string GetFilterSessionID()
        {
            return "cinemabillboard-subscriptions-management-filter";
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerViewer)]
        public override IActionResult Filter(SubscriptionFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerCreator)]
        public override IActionResult Create(SubscriptionInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    ModelState.ClearValidationState("ImageFile");

                    modelInput.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerEditor)]
        public override IActionResult Edit(string key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerEditor)]
        public override IActionResult Edit(SubscriptionInputViewModel modelInput)
        {
            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    modelInput.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerEditor)]
        public override ActionResult Enable(string key)
        {
            return base.Enable(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerEditor)]
        public override ActionResult Disable(string key)
        {
            return base.Disable(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerRemover)]
        public override IActionResult Delete(string key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerRemover)]
        public override IActionResult Delete(SubscriptionDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Subscription entity, SubscriptionInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            if (modelInput.ImageFile != null)
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.ImageFile.CopyTo(memoryStream);

                    var image = new Middleware.Media.Persistence.Entities.Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Subscription " + entity.Id };

                    mediaFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray());

                    entity.Identity.Image = image;
                }
            }
        }

        protected override void PreEdit(Subscription entity, SubscriptionInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.Identity.Image != null)
                {
                    mediaFileManager.DeleteImageFile(entity.Identity.Image.Id);

                    imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
                }
            }
            else
            {
                if (entity.Identity.Image != null)
                {
                    mediaFileManager.DeleteImageFile(entity.Identity.Image.Id);

                    imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
                }

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.ImageFile.CopyTo(memoryStream);

                    var image = new Middleware.Media.Persistence.Entities.Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Subscription " + entity.Id };

                    mediaFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray());

                    entity.Identity.Image = image;
                }
            }
        }

        protected override void PostDelete(Subscription entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.Identity, User.Identity.Name, false);

            if (entity.Identity.Image != null)
            {
                imageRepository.Delete(entity.Identity.Image, User.Identity.Name, false);
            }

            if (entity.Identity.Image != null)
            {
                mediaFileManager.DeleteImageFile(entity.Identity.Image.Id);
            }
        }
    }
}
