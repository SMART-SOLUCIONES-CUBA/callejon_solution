using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.Filter;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Facades.Manager;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cinemabillboard")]
    public class ProducerHousesController : Core.Web.Management.Controllers.AbstractEntityManagementFilteredController<ProducerHouse, long, ProducerHouseInputViewModel, ProducerHouseDisplayViewModel, ProducerHouseFilterModel>
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IMediaFileManagerFacade mediaFileManagerFacade;

        public ProducerHousesController(IMediaFileManagerFacade mediaFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<ProducerHouse, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<ProducerHousesController> localizer, ILogger<ProducerHousesController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.mediaFileManagerFacade = mediaFileManagerFacade;
        }

        protected override string GetFilterSessionID()
        {
            return "cinemabillboard-producerhouses-management-filter";
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerViewer)]
        public override IActionResult Filter(ProducerHouseFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerCreator)]
        public override IActionResult Create(ProducerHouseInputViewModel modelInput)
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

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerEditor)]
        public override IActionResult Edit(long key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerEditor)]
        public override IActionResult Edit(ProducerHouseInputViewModel modelInput)
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

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerRemover)]
        public override IActionResult Delete(long key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerRemover)]
        public override IActionResult Delete(ProducerHouseDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(ProducerHouse entity, ProducerHouseInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "ProducerHouse " + entity.Id };

                mediaFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray());

                entity.Image = image;
            }
        }

        protected override void PreEdit(ProducerHouse entity, ProducerHouseInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.Image != null)
                {
                    mediaFileManagerFacade.DeleteImageFile(entity.Image.Id);

                    imageRepository.Delete(entity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.Image != null)
            {
                mediaFileManagerFacade.DeleteImageFile(entity.Image.Id);

                imageRepository.Delete(entity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "ProducerHouse " + entity.Id };

                mediaFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray());

                entity.Image = image;
            }
        }

        protected override void PreDelete(ProducerHouse entity)
        {
            base.PreDelete(entity);

            if (entity.Image == null) return;

            mediaFileManagerFacade.DeleteImageFile(entity.Image.Id);

            imageRepository.Delete(entity.Image, User.Identity.Name);
        }
    }
}
