using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Common.Extensions;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.Filter;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
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
            var modelInput = new SubscriptionCreateViewModel();

            ConfigureCreate(modelInput);

            return View(modelInput);
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

            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Request.Form.TryGetValue("Password", out Microsoft.Extensions.Primitives.StringValues passwords);
            Request.Form.TryGetValue("ConfirmPassword", out Microsoft.Extensions.Primitives.StringValues confirmations);

            modelInput = new SubscriptionCreateViewModel
            {
                Password = passwords.Count > 0 ? passwords.ToArray()[0] : string.Empty,
                ConfirmPassword = confirmations.Count > 0 ? confirmations.ToArray()[0] : string.Empty,
                Enabled = modelInput.Enabled,
                Id = modelInput.Id,
                Name = modelInput.Name,
                LastName = modelInput.LastName,
                About = modelInput.About,
                ImageFile = modelInput.ImageFile,
                ImageId = modelInput.ImageId,
                PasswordChangeRequired = modelInput.PasswordChangeRequired,
                ServiceAvailabilityFrom = modelInput.ServiceAvailabilityFrom,
                ServiceAvailabilityTo = modelInput.ServiceAvailabilityTo
            };

            ValidateCreate(modelInput);

            if (TryValidateModel(modelInput))
            {
                var entity = modelInput.Export();

                PreCreate(entity, modelInput);

                try
                {
                    Repository.Add(entity, User.Identity.Name);

                    PostCreate(entity, modelInput);

                    notification.Type = NotificationType.Success;
                    notification.Message = GetCreationSuccessMessage();

                    TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

                    return RedirectToAction(GetDefaultActionName());
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    logger.LogError(e.Message, e.InnerException);

                    ModelState.AddModelError("Id", "UserNameTakedMessage");
                }
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

            return View(modelInput);
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

            entity.Password = entity.Password.ToHashedPassword();

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

        protected override void ValidateCreate(SubscriptionInputViewModel modelInput)
        {
            base.ValidateCreate(modelInput);

            if (modelInput.ServiceAvailabilityTo.Date <= System.DateTime.Now.Date)
            {
                ModelState.AddModelError("ServiceAvailabilityTo", localizer["ServiceAvailabilityToInvalidMesssage"]);
            }

            if (modelInput.ServiceAvailabilityTo <= modelInput.ServiceAvailabilityFrom)
            {
                ModelState.AddModelError("ServiceAvailabilityTo", localizer["ServiceAvailabilityToCompareMesssage"]);
                ModelState.AddModelError("ServiceAvailabilityFrom", localizer["ServiceAvailabilityFromCompareMesssage"]);
            }
        }

        protected override void ValidateEdit(SubscriptionInputViewModel modelInput)
        {
            base.ValidateEdit(modelInput);

            if (modelInput.ServiceAvailabilityTo.Date <= System.DateTime.Now.Date)
            {
                ModelState.AddModelError("ServiceAvailabilityTo", localizer["ServiceAvailabilityToInvalidMesssage"]);
            }

            if (modelInput.ServiceAvailabilityTo <= modelInput.ServiceAvailabilityFrom)
            {
                ModelState.AddModelError("ServiceAvailabilityTo", localizer["ServiceAvailabilityToCompareMesssage"]);
                ModelState.AddModelError("ServiceAvailabilityFrom", localizer["ServiceAvailabilityFromCompareMesssage"]);
            }
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(SubscriptionChangePasswordViewModel model)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Warning,
                Title = "Cambiar Contraseña.",
                Message = "Los datos especificados no son correctos."
            };

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = Repository.Get(model.Id);

                    entity.Password = model.NewPassword.ToHashedPassword();
                    entity.PasswordChangeRequired = model.PasswordChangeRequired;

                    Repository.Update(entity, User.Identity.Name);

                    notification.Type = NotificationType.Success;
                    notification.Message = "Contraseña modificada con éxito.";
                }
                catch
                {
                    notification.Type = NotificationType.Danger;
                    notification.Message = GetNotFoundMessage();
                }
            }
            else
            {
                var validationErrorMessages = new System.Collections.Generic.List<string>();

                foreach (var record in ModelState)
                {
                    foreach (var error in record.Value.Errors)
                    {
                        validationErrorMessages.Add(error.ErrorMessage);
                    }
                }

                notification.Type = NotificationType.Danger;
                notification.Title = "Cambiar Contraseña." + (validationErrorMessages.Count > 1 ? " Múltiples errores." : string.Empty);
                notification.Messages = validationErrorMessages.ToArray();
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

            return RedirectToAction("index");
        }
    }
}
