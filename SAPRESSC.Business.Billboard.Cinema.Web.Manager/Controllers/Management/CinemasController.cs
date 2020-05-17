using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Facades.Manager;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cinemabillboard")]
    public class CinemasController : Core.Web.Management.Controllers.AbstractEntityManagementController<Persistence.Entities.Cinema, long, CinemaInputViewModel, CinemaDisplayViewModel>
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IMediaFileManagerFacade mediaFileManagerFacade;
        private readonly IEntityRepository<Room, System.Guid> roomRepository;
        private readonly IEntityRepository<EmailContactItem, long> emailContactItemRepository;
        private readonly IEntityRepository<PhoneContactItem, long> phoneContactItemRepository;

        public CinemasController(IEntityRepository<PhoneContactItem, long> phoneContactItemRepository, IEntityRepository<EmailContactItem, long> emailContactItemRepository, IEntityRepository<Room, System.Guid> roomRepository, IMediaFileManagerFacade mediaFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Persistence.Entities.Cinema, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<CinemasController> localizer, ILogger<CinemasController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.mediaFileManagerFacade = mediaFileManagerFacade;
            this.roomRepository = roomRepository;
            this.emailContactItemRepository = emailContactItemRepository;
            this.phoneContactItemRepository = phoneContactItemRepository;
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerCreator)]
        public override IActionResult Create(CinemaInputViewModel modelInput)
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

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        public override IActionResult Edit(long key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        public override IActionResult Edit(CinemaInputViewModel modelInput)
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

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerRemover)]
        public override IActionResult Delete(long key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerRemover)]
        public override IActionResult Delete(CinemaDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Persistence.Entities.Cinema entity, CinemaInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Cinema " + entity.Id };

                mediaFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray());

                entity.Image = image;
            }
        }

        protected override void PreEdit(Persistence.Entities.Cinema entity, CinemaInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (modelInput.ImageFile == null) return;

            mediaFileManagerFacade.DeleteImageFile(entity.Image.Id);

            imageRepository.Delete(entity.Image, User.Identity.Name);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Cinema " + entity.Id };

                mediaFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray());

                entity.Image = image;
            }
        }

        protected override void PostDelete(Persistence.Entities.Cinema entity)
        {
            base.PostDelete(entity);

            mediaFileManagerFacade.DeleteImageFile(entity.Image.Id);

            imageRepository.Delete(entity.Image, User.Identity.Name);
        }

        protected override CinemaInputViewModel ConfigurePostFailEditValidation(CinemaInputViewModel modelInput)
        {
            var entity = Repository.Get(modelInput.Id);

            modelInput.ImageId = entity.Image.Id;

            return base.ConfigurePostFailEditValidation(modelInput);
        }

        [Route("edit/{key}/[action]")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        public IActionResult Contacts(long key)
        {
            try
            {
                var entity = Repository.Get(key);

                var modelDisplay = new CinemaDisplayViewModel();

                modelDisplay.Import(entity);

                PopulateViewModelDisplayExtras(modelDisplay, entity);

                return View(modelDisplay);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction(GetDefaultActionName());
        }

        [Route("edit/contacts/email/add")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmail(EmailContactInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Persistence.Entities.Cinema cinema = null;

            try
            {
                cinema = Repository.Get(modelInput.CinemaId);
            }
            catch { }

            if (cinema == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var entity = modelInput.Export();

                emailContactItemRepository.Add(entity, User.Identity.Name);

                cinema.OrganizationEmailContactItemBridges.Add(new OrganizationEmailContactItemBridge { ContactItem = entity, ContactItemId = entity.Id, Organization = cinema, OrganizationId = cinema.Id });

                Repository.Update(cinema, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetCreationSuccessMessage();
            }
            else
            {
                notification.Messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        notification.Messages.Add(error.ErrorMessage);
                    }
                }
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("contacts", new { key = cinema.Id });
        }

        [Route("edit/contacts/email/edit")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmail(EmailContactInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Persistence.Entities.Cinema cinema = null;

            try
            {
                cinema = Repository.Get(modelInput.CinemaId);
            }
            catch { }

            if (cinema == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var entity = cinema.GetEmailContactItems().FirstOrDefault(e => e.Id == modelInput.Id);

                if (entity != null)
                {
                    modelInput.Mergue(entity);

                    emailContactItemRepository.Update(entity, User.Identity.Name);

                    notification.Type = NotificationType.Success;
                    notification.Message = GetEditionSuccessMessage();
                }
                else
                {
                    notification.Message = GetNotFoundMessage();
                }
            }
            else
            {
                notification.Messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        notification.Messages.Add(error.ErrorMessage);
                    }
                }
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("contacts", new { key = cinema.Id });
        }

        [Route("edit/contacts/email/delete")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmail(EmailContactInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            Persistence.Entities.Cinema cinema = null;

            try
            {
                cinema = Repository.Get(modelInput.CinemaId);
            }
            catch { }

            if (cinema == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            try
            {
                var entity = cinema.GetEmailContactItems().FirstOrDefault(e => e.Id == modelInput.Id);

                emailContactItemRepository.Delete(entity, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetDeletionSuccessMessage();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message, null);

                notification.Type = NotificationType.Warning;
                notification.Message = GetOnDeletionDependecyConstrainErrorMessage();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message, null);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("contacts", new { key = cinema.Id });
        }

        [Route("edit/contacts/phone/add")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPhone(PhoneContactInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Persistence.Entities.Cinema cinema = null;

            try
            {
                cinema = Repository.Get(modelInput.CinemaId);
            }
            catch { }

            if (cinema == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var entity = modelInput.Export();

                phoneContactItemRepository.Add(entity, User.Identity.Name);

                cinema.OrganizationPhoneContactItemBridges.Add(new OrganizationPhoneContactItemBridge { ContactItem = entity, ContactItemId = entity.Id, Organization = cinema, OrganizationId = cinema.Id });

                Repository.Update(cinema, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetCreationSuccessMessage();
            }
            else
            {
                notification.Messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        notification.Messages.Add(error.ErrorMessage);
                    }
                }
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("contacts", new { key = cinema.Id });
        }

        [Route("edit/contacts/phone/edit")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPhone(PhoneContactInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Persistence.Entities.Cinema cinema = null;

            try
            {
                cinema = Repository.Get(modelInput.CinemaId);
            }
            catch { }

            if (cinema == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var entity = cinema.GetPhoneContactItems().FirstOrDefault(e => e.Id == modelInput.Id);

                if (entity != null)
                {
                    modelInput.Mergue(entity);

                    phoneContactItemRepository.Update(entity, User.Identity.Name);

                    notification.Type = NotificationType.Success;
                    notification.Message = GetEditionSuccessMessage();
                }
                else
                {
                    notification.Message = GetNotFoundMessage();
                }
            }
            else
            {
                notification.Messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        notification.Messages.Add(error.ErrorMessage);
                    }
                }
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("contacts", new { key = cinema.Id });
        }

        [Route("edit/contacts/phone/delete")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePhone(PhoneContactInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            Persistence.Entities.Cinema cinema = null;

            try
            {
                cinema = Repository.Get(modelInput.CinemaId);
            }
            catch { }

            if (cinema == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            try
            {
                var entity = cinema.GetPhoneContactItems().FirstOrDefault(e => e.Id == modelInput.Id);

                phoneContactItemRepository.Delete(entity, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetDeletionSuccessMessage();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message, null);

                notification.Type = NotificationType.Warning;
                notification.Message = GetOnDeletionDependecyConstrainErrorMessage();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message, null);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("contacts", new { key = cinema.Id });
        }

        [Route("edit/{key}/[action]")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        public IActionResult Rooms(long key)
        {
            try
            {
                var entity = Repository.Get(key);

                var modelDisplay = new CinemaDisplayViewModel();

                modelDisplay.Import(entity);

                PopulateViewModelDisplayExtras(modelDisplay, entity);

                return View(modelDisplay);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message, null);
            }

            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction(GetDefaultActionName());
        }

        [Route("edit/rooms/add")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRoom(RoomInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Persistence.Entities.Cinema cinema = null;

            try
            {
                cinema = Repository.Get(modelInput.CinemaId);
            }
            catch { }

            if (cinema == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var entity = modelInput.Export();

                entity.Cinema = cinema;

                if (cinema.Rooms == null)
                {
                    cinema.Rooms = new List<Room>();
                }

                cinema.Rooms.Add(entity);

                Repository.Update(cinema, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetCreationSuccessMessage();
            }
            else
            {
                notification.Messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        notification.Messages.Add(error.ErrorMessage);
                    }
                }
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("rooms", new { key = cinema.Id });
        }

        [Route("edit/rooms/edit")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRoom(RoomInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Persistence.Entities.Cinema cinema = null;

            try
            {
                cinema = Repository.Get(modelInput.CinemaId);
            }
            catch { }

            if (cinema == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (ModelState.IsValid)
            {
                var entity = roomRepository.FindOneBy(e => e.Id == modelInput.Id && e.Cinema.Id == cinema.Id);

                if (entity != null)
                {
                    modelInput.Mergue(entity);

                    roomRepository.Update(entity, User.Identity.Name);

                    notification.Type = NotificationType.Success;
                    notification.Message = GetEditionSuccessMessage();
                }
                else
                {
                    notification.Message = GetNotFoundMessage();
                }
            }
            else
            {
                notification.Messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        notification.Messages.Add(error.ErrorMessage);
                    }
                }
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("rooms", new { key = cinema.Id });
        }

        [Route("edit/rooms/delete")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRoom(RoomInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            Persistence.Entities.Cinema cinema = null;

            try
            {
                cinema = Repository.Get(modelInput.CinemaId);
            }
            catch { }

            if (cinema == null)
            {
                notification.Message = GetNotFoundMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            try
            {
                var entity = roomRepository.FindOneBy(e => e.Id == modelInput.Id && e.Cinema.Id == cinema.Id);

                roomRepository.Delete(entity, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetDeletionSuccessMessage();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message, null);

                notification.Type = NotificationType.Warning;
                notification.Message = GetOnDeletionDependecyConstrainErrorMessage();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message, null);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("rooms", new { key = cinema.Id });
        }
    }
}
