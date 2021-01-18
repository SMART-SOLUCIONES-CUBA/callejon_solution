using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Facades.Manager;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cinemabillboard")]
    public class AdvertisementsController : Core.Web.Management.Controllers.AbstractEntityManagementController<Persistence.Entities.Advertisement, System.Guid, AdvertisementInputViewModel, AdvertisementDisplayViewModel>
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IMediaFileManagerFacade mediaFileManagerFacade;

        public AdvertisementsController(IMediaFileManagerFacade mediaFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Persistence.Entities.Advertisement, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<AdvertisementsController> localizer, ILogger<AdvertisementsController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.mediaFileManagerFacade = mediaFileManagerFacade;
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AdvertisementManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AdvertisementManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AdvertisementManagerCreator)]
        public override IActionResult Create(AdvertisementInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    ModelState.ClearValidationState("PlacardImageFile");

                    modelInput.PlacardImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AdvertisementManagerEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AdvertisementManagerEditor)]
        public override IActionResult Edit(AdvertisementInputViewModel modelInput)
        {
            ModelState.ClearValidationState("PlacardImageFile");
            ModelState.MarkFieldValid("PlacardImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    modelInput.PlacardImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AdvertisementManagerRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.AdvertisementManagerRemover)]
        public override IActionResult Delete(AdvertisementDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Persistence.Entities.Advertisement entity, AdvertisementInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.PlacardImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.PlacardImageFile.ContentType, Length = modelInput.PlacardImageFile.Length, About = "Advertisement " + entity.Id };

                mediaFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray());

                entity.PlacardImage = image;
            }
        }

        protected override void PreEdit(Persistence.Entities.Advertisement entity, AdvertisementInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (modelInput.PlacardImageFile == null) return;

            mediaFileManagerFacade.DeleteImageFile(entity.PlacardImage.Id);

            imageRepository.Delete(entity.PlacardImage, User.Identity.Name);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.PlacardImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.PlacardImageFile.ContentType, Length = modelInput.PlacardImageFile.Length, About = "Advertisement " + entity.Id };

                mediaFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray());

                entity.PlacardImage = image;
            }
        }

        protected override void PostDelete(Persistence.Entities.Advertisement entity)
        {
            base.PostDelete(entity);

            mediaFileManagerFacade.DeleteImageFile(entity.PlacardImage.Id);

            imageRepository.Delete(entity.PlacardImage, User.Identity.Name);
        }

        protected override AdvertisementInputViewModel ConfigurePostFailEditValidation(AdvertisementInputViewModel modelInput)
        {
            var entity = Repository.Get(modelInput.Id);

            modelInput.PlacardImageId = entity.PlacardImage.Id;

            return base.ConfigurePostFailEditValidation(modelInput);
        }
    }
}