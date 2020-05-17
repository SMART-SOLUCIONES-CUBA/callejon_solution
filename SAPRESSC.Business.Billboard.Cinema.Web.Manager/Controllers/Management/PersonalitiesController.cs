using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.Filter;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Facades.Manager;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cinemabillboard")]
    public class PersonalitiesController : Core.Web.Management.Controllers.AbstractEntityManagementFilteredController<Personality, long, PersonalityInputViewModel, PersonalityDisplayViewModel, PersonalityFilterModel>
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IMediaFileManagerFacade mediaFileManagerFacade;

        public PersonalitiesController(IMediaFileManagerFacade mediaFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Personality, long> repository, IStringLocalizer<PersonalitiesController> localizer, ILogger<PersonalitiesController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.mediaFileManagerFacade = mediaFileManagerFacade;
        }

        protected override string GetFilterSessionID()
        {
            return "cinemabillboard-personalities-management-filter";
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerViewer)]
        public override IActionResult Filter(PersonalityFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerCreator)]
        public override IActionResult Create(PersonalityInputViewModel modelInput)
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

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerEditor)]
        public override IActionResult Edit(long key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerEditor)]
        public override IActionResult Edit(PersonalityInputViewModel modelInput)
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

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerRemover)]
        public override IActionResult Delete(long key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerRemover)]
        public override IActionResult Delete(PersonalityDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Personality entity, PersonalityInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Personality " + entity.Id };

                mediaFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray());

                entity.Image = image;
            }
        }

        protected override void PreEdit(Personality entity, PersonalityInputViewModel modelInput)
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

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Personality " + entity.Id };

                mediaFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray());

                entity.Image = image;
            }
        }

        protected override void PreDelete(Personality entity)
        {
            base.PreDelete(entity);

            if (entity.Image == null) return;

            mediaFileManagerFacade.DeleteImageFile(entity.Image.Id);

            imageRepository.Delete(entity.Image, User.Identity.Name);
        }

        protected override PersonalityInputViewModel ConfigurePostFailEditValidation(PersonalityInputViewModel modelInput)
        {
            var entity = Repository.Get(modelInput.Id);

            if (entity.Image != null)
            {
                modelInput.ImageId = entity.Image.Id;
            }

            return base.ConfigurePostFailEditValidation(modelInput);
        }

        protected override void ValidateCreate(PersonalityInputViewModel modelInput)
        {
            base.ValidateCreate(modelInput);

            if (Repository.GetAll().Any(entity => entity.Name == modelInput.Name && entity.LastName == modelInput.LastName))
            {
                ModelState.AddModelError("Name", localizer["UniqueValueConvinationValidationErrorMessage", localizer["Name"], localizer["LastName"]]);
                ModelState.AddModelError("LastName", localizer["UniqueValueConvinationValidationErrorMessage", localizer["Name"], localizer["LastName"]]);
            }
        }

        protected override void ValidateEdit(PersonalityInputViewModel modelInput)
        {
            base.ValidateEdit(modelInput);

            if (Repository.GetAll().Any(entity => entity.Id != modelInput.Id && entity.Name == modelInput.Name && entity.LastName == modelInput.LastName))
            {
                ModelState.AddModelError("Name", localizer["UniqueValueConvinationValidationErrorMessage", localizer["Name"], localizer["LastName"]]);
                ModelState.AddModelError("LastName", localizer["UniqueValueConvinationValidationErrorMessage", localizer["Name"], localizer["LastName"]]);
            }
        }
    }
}
