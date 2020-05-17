using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.Filter;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cinemabillboard")]
    public class PresentationsController : Core.Web.Management.Controllers.AbstractHabilitableEntityManagementFilteredController<Presentation, long, PresentationInputViewModel, PresentationDisplayViewModel, PresentationFilterModel>
    {
        private readonly IEntityRepository<Production, long> productionRepository;
        private readonly IEntityRepository<Room, Guid> roomRepository;

        public PresentationsController(IEntityRepository<Room, Guid> roomRepository, IEntityRepository<Production, long> productionRepository, IEntityRepository<Presentation, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<PresentationsController> localizer, ILogger<PresentationsController> logger) : base(repository, localizer, logger)
        {
            this.productionRepository = productionRepository;
            this.roomRepository = roomRepository;
        }

        protected override string GetFilterSessionID()
        {
            return "cinemabillboard-presentations-management-filter";
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerViewer)]
        public override IActionResult Filter(PresentationFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerCreator)]
        public override IActionResult Create()
        {
            if (roomRepository.Count() <= 0)
            {
                var notification = new NotificationViewModel
                {
                    Type = NotificationType.Warning,
                    Title = "RequiredDataIncompleteTitle",
                    Message = "RequiredDataIncompleteMessage",
                    Messages = new string[] { "AtLastOneRoomRequiredMessage" }
                };

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            if (!Request.Query.ContainsKey(PresentationInputViewModel.PRODUCTIONID_QUERY_PARAM) || !long.TryParse(Request.Query.First(item => item.Key == PresentationInputViewModel.PRODUCTIONID_QUERY_PARAM).Value, out long productionId) || productionRepository.FindOneBy(item => item.Id == productionId) == null)
            {
                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(new NotificationViewModel
                {
                    Type = NotificationType.Warning,
                    Title = "RequiredDataIncompleteTitle",
                    Message = "ProductionPickUpRequiredMessage"
                });

                return RedirectToAction("pickproduction");
            }

            return base.Create();
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerCreator)]
        public override IActionResult Create(PresentationInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerEditor)]
        public override IActionResult Edit(long key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerEditor)]
        public override IActionResult Edit(PresentationInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerEditor)]
        public override ActionResult Enable(long key)
        {
            return base.Enable(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerEditor)]
        public override ActionResult Disable(long key)
        {
            return base.Disable(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerRemover)]
        public override IActionResult Delete(long key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerRemover)]
        public override IActionResult Delete(PresentationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private static readonly string PickPresentationProductionFilterSessionID = "cinemabillboard-presentations-production-pick-filter";

        [HttpGet]
        [Route("create/[action]")]
        [Route("create/[action]/{key}")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerCreator)]
        public IActionResult PickProduction(int key = 1)
        {
            ProductionFilterModel filterModel = null;

            try
            {
                filterModel = JsonConvert.DeserializeObject<ProductionFilterModel>(HttpContext.Session.GetString(PickPresentationProductionFilterSessionID));
            }
            catch { }

            var limit = 25;

            System.Linq.Expressions.Expression<Func<Production, bool>> selector = entity => entity.Participations.Any(item => item.Role == ProductionRole.Direction);

            if (filterModel != null && filterModel.Title != null && filterModel.Title != string.Empty)
            {
                selector = entity => entity.Participations.Any(item => item.Role == ProductionRole.Direction && (entity.Title.Contains(filterModel.Title) || entity.LocalizedTitle.Contains(filterModel.Title)));
            }

            var maxCount = productionRepository.FindBy(selector).Count();

            if (maxCount == 0)
            {
                return View(new PaginationViewModel<ProductionDisplayViewModel>
                {
                    Limit = limit,
                    Page = 0,
                    HasNext = false,
                    HasPrevious = false,
                    List = new List<ProductionDisplayViewModel>(),
                    Filter = filterModel
                });
            }

            var rest = maxCount % limit;

            var maxPageCount = maxCount / limit;

            if (rest > 0)
                maxPageCount += 1;

            if (key < 0 || key > maxPageCount)
            {
                throw new InvalidOperationException();
            }

            var entities = productionRepository.FindBy(selector).Skip((key - 1) * limit).Take(limit).ToList();

            var models = new List<ProductionDisplayViewModel>();

            foreach (var entity in entities)
            {
                var model = new ProductionDisplayViewModel();

                model.Import(entity);

                models.Add(model);
            }

            var result = new PaginationViewModel<ProductionDisplayViewModel>
            {
                Limit = limit,
                Page = key,
                HasPrevious = key > 1,
                HasNext = key < maxPageCount,
                MaxPageCount = maxPageCount,
                Action = ControllerContext.ActionDescriptor.ActionName,
                RouteValues = ControllerContext.ActionDescriptor.RouteValues,
                List = models,
                Filter = filterModel
            };

            return View(result);
        }

        [HttpPost]
        [Route("create/[action]")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerViewer)]
        public IActionResult PickProduction(ProductionFilterModel filterModel)
        {
            if (filterModel != null)
            {
                HttpContext.Session.SetString(PickPresentationProductionFilterSessionID, JsonConvert.SerializeObject(filterModel));
            }

            return RedirectToAction("pickproduction");
        }

        private void PopulateModelInputForAvailableRooms(PresentationInputViewModel inputModel)
        {
            var entities = roomRepository.GetAll().OrderBy(item => item.Cinema.Id);

            var selectListItems = new List<SelectListItem>();

            SelectListGroup group = null;

            foreach (var entity in entities)
            {
                if (group == null || group.Name != entity.Cinema.Name)
                {
                    group = new SelectListGroup { Name = entity.Cinema.Name };
                }

                selectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Group = group });
            }

            inputModel.Rooms = selectListItems;
        }

        protected override PresentationInputViewModel ConfigureCreate(PresentationInputViewModel modelInput)
        {
            var productionId = long.Parse(Request.Query.First(item => item.Key == PresentationInputViewModel.PRODUCTIONID_QUERY_PARAM).Value);

            modelInput.ProductionId = productionId;
            modelInput.Production = productionRepository.Get(productionId);

            PopulateModelInputForAvailableRooms(modelInput);

            return base.ConfigureCreate(modelInput);
        }

        protected override PresentationInputViewModel ConfigureEdit(Presentation entity, PresentationInputViewModel modelInput)
        {
            modelInput.Production = entity.Production;

            PopulateModelInputForAvailableRooms(modelInput);

            return base.ConfigureEdit(entity, modelInput);
        }

        protected override PresentationInputViewModel ConfigurePostFailCreateValidation(PresentationInputViewModel modelInput)
        {
            modelInput.Production = productionRepository.Get(modelInput.ProductionId);

            PopulateModelInputForAvailableRooms(modelInput);

            return base.ConfigurePostFailCreateValidation(modelInput);
        }

        protected override PresentationInputViewModel ConfigurePostFailEditValidation(PresentationInputViewModel modelInput)
        {
            modelInput.Production = productionRepository.Get(modelInput.ProductionId);

            PopulateModelInputForAvailableRooms(modelInput);

            return base.ConfigurePostFailEditValidation(modelInput);
        }

        protected override void PreCreate(Presentation entity, PresentationInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.Production = productionRepository.Get(modelInput.ProductionId);
            entity.Room = roomRepository.Get(modelInput.RoomId);
        }

        protected override void PostCreate(Presentation entity, PresentationInputViewModel modelInput)
        {
            base.PostCreate(entity, modelInput);

            HttpContext.Session.Remove(PickPresentationProductionFilterSessionID);
        }

        protected override void PreEdit(Presentation entity, PresentationInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            entity.Room = roomRepository.Get(modelInput.RoomId);
        }

        protected override void ValidateCreate(PresentationInputViewModel modelInput)
        {
            base.ValidateCreate(modelInput);

            if (modelInput.RoomId == Guid.Empty)
            {
                ModelState.AddModelError("RoomId", localizer["RoomSelectionRequiredMessage"]);
            }
            else
            {
                if (Repository.FindBy(e => e.Room.Id == modelInput.RoomId).Any(e => e.Date == modelInput.PresentationDate))
                {
                    ModelState.AddModelError("PresentationDate", localizer["PresentationDateTimeColisionMesssage"]);
                }
            }
        }

        protected override void ValidateEdit(PresentationInputViewModel modelInput)
        {
            base.ValidateEdit(modelInput);

            if (modelInput.RoomId == Guid.Empty)
            {
                ModelState.AddModelError("RoomId", "RoomSelectionRequiredMessage");
            }
            else
            {
                if (Repository.FindBy(e => e.Room.Id == modelInput.RoomId).Any(e => e.Date == modelInput.PresentationDate))
                {
                    ModelState.AddModelError("PresentationDate", "PresentationDateTimeColisionMesssage");
                }
            }
        }

        [HttpGet]
        [Route("[action]/{key}")]
        [Route("[action]/{key}/{page}")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerEditor)]
        public IActionResult ChangeProduction(long key, int page = 1)
        {
            try
            {
                var entity = Repository.Get(key);

                var baseModelInput = new PresentationInputViewModel();

                baseModelInput.Import(entity);

                baseModelInput = ConfigureEdit(entity, baseModelInput);

                var modelInput = new PresentationChangeProductionInputViewModel
                {
                    Id = baseModelInput.Id,
                    PresentationDate = baseModelInput.PresentationDate,
                    Production = baseModelInput.Production,
                    ProductionId = baseModelInput.ProductionId,
                    RoomId = baseModelInput.RoomId,
                    Rooms = baseModelInput.Rooms
                };

                var limit = 10;

                System.Linq.Expressions.Expression<Func<Production, bool>> selector = e => e.Participations.Any(item => item.Role == ProductionRole.Direction) && e.Participations.Any(item => item.Role == ProductionRole.Interpretation);

                var maxCount = productionRepository.FindBy(selector).Count();

                if (maxCount == 0)
                {
                    modelInput.ProductionsPagination = new PaginationViewModel<ProductionDisplayViewModel>
                    {
                        Limit = limit,
                        Page = 0,
                        HasNext = false,
                        HasPrevious = false,
                        List = new List<ProductionDisplayViewModel>()
                    };

                    return View(modelInput);
                }

                var rest = maxCount % limit;

                var maxPageCount = maxCount / limit;

                if (rest > 0)
                    maxPageCount += 1;

                if (page < 0 || page > maxPageCount)
                {
                    throw new InvalidOperationException();
                }

                var entities = productionRepository.FindBy(selector).Skip((page - 1) * limit).Take(limit).ToList();

                var models = new List<ProductionDisplayViewModel>();

                foreach (var e in entities)
                {
                    var model = new ProductionDisplayViewModel();

                    model.Import(e);

                    models.Add(model);
                }

                modelInput.ProductionsPagination = new PaginationViewModel<ProductionDisplayViewModel>
                {
                    Limit = limit,
                    Page = page,
                    HasPrevious = page > 1,
                    HasNext = page < maxPageCount,
                    MaxPageCount = maxPageCount,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    RouteValues = ControllerContext.ActionDescriptor.RouteValues,
                    List = models
                };

                return View(modelInput);
            }
            catch (Exception e)
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

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerEditor)]
        public IActionResult ChangeProduction(PresentationInputViewModel modelInput)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            try
            {
                var entity = Repository.Get(modelInput.Id);

                entity.Production = productionRepository.Get(modelInput.ProductionId);

                Repository.Update(entity, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetEditionSuccessMessage();

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction("edit", new { key = entity.Id });
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message, null);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction(GetDefaultActionName());
        }

        [HttpGet]
        [Route("[action]")]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerViewer)]
        public IActionResult Calendar()
        {
            var filter = new CalendarFilterModel
            {
                FirstDate = DateTime.Today,
                LastDate = DateTime.Today.AddDays(6)
            };

            Room currentRoom = null;

            try
            {
                filter = JsonConvert.DeserializeObject<CalendarFilterModel>(HttpContext.Session.GetString(Constants.CALENDAR_FILTER_PARAMS_MAP_KEY));

                currentRoom = roomRepository.Get(filter.RoomId);
            }
            catch
            {
                currentRoom = roomRepository.GetAll().FirstOrDefault();

                if (currentRoom != null)
                {
                    filter.RoomId = currentRoom.Id;
                }
            }

            var selectListItems = new List<SelectListItem>();

            SelectListGroup group = null;

            var entities = roomRepository.GetAll().OrderBy(item => item.Cinema.Id);

            foreach (var entity in entities)
            {
                if (group == null || group.Name != entity.Cinema.Name)
                {
                    group = new SelectListGroup { Name = entity.Cinema.Name };
                }

                selectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Group = group });
            }

            filter.Rooms = selectListItems;

            var model = new CalendarViewModel { Filter = filter, DailyPresentationsBillboards = new List<DailyPresentationsBillboardViewModel>() };

            if (currentRoom != null)
            {
                var currentDate = filter.FirstDate;

                while (currentDate <= filter.LastDate)
                {
                    var billboard = new DailyPresentationsBillboardViewModel { Date = currentDate, Presentations = currentRoom.Presentations.Where(e => e.Date.Date == currentDate).OrderBy(e => e.Date).ToList() };

                    model.DailyPresentationsBillboards.Add(billboard);

                    currentDate = currentDate.AddDays(1);
                }
            }

            return View(model);
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerViewer)]
        public IActionResult Calendar(CalendarFilterModel model)
        {
            if (model.RoomId == null)
            {
                ModelState.AddModelError("RoomId", localizer["RoomSelectionRequiredMessage"]);
            }

            if (roomRepository.FindOneBy(e => e.Id == model.RoomId) == null)
            {
                ModelState.AddModelError("RoomId", localizer["RoomNotExistMessage"]);
            }

            if (model.FirstDate > model.LastDate)
            {
                ModelState.AddModelError("LastDate", localizer["DateTimeRangeInvalidMessage"]);
            }

            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString(Constants.CALENDAR_FILTER_PARAMS_MAP_KEY, JsonConvert.SerializeObject(model));
            }
            else
            {
                var notification = new NotificationViewModel
                {
                    Type = NotificationType.Danger,
                    Message = GetInvalidMessage(),
                    Messages = new List<string>()
                };

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        notification.Messages.Add(error.ErrorMessage);
                    }
                }

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);
            }

            return RedirectToAction("calendar");
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerCreator)]
        public IActionResult Duplicate(PresentationDuplicateInputViewModel inputModel)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            try
            {
                var entity = Repository.Get(inputModel.PresentationId);

                if (Repository.FindBy(e => e.Room.Id == entity.Room.Id).Any(e => e.Date == inputModel.PresentationDate))
                {
                    ModelState.AddModelError("PresentationDate", localizer["PresentationDateTimeColisionMesssage"]);
                }

                if (ModelState.IsValid)
                {
                    var duplicate = new Presentation
                    {
                        Date = inputModel.PresentationDate,
                        Production = entity.Production,
                        Room = roomRepository.Get(inputModel.RoomId)
                    };

                    Repository.Add(duplicate, User.Identity.Name);

                    notification.Type = NotificationType.Success;
                    notification.Message = GetCreationSuccessMessage();
                }
                else
                {
                    notification.Message = GetInvalidMessage();
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
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message, null);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("calendar");
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerCreator)]
        public IActionResult Expand(PresentationExpandInputViewModel inputModel)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            try
            {
                var entity = Repository.Get(inputModel.PresentationId);

                if (inputModel.PresentationStartDate > inputModel.PresentationEndDate)
                {
                    ModelState.AddModelError("PresentationEndDate", localizer["DateTimeRangeInvalidMessage"]);
                }

                if (ModelState.IsValid)
                {
                    var currentDate = inputModel.PresentationStartDate;

                    while (currentDate <= inputModel.PresentationEndDate)
                    {
                        var presentationDate = currentDate.Add(inputModel.PresentationTime.TimeOfDay);

                        var targetPresentation = Repository.FindOneBy(e => e.Date == presentationDate && e.Room.Id == entity.Room.Id);

                        if (!(targetPresentation != null && targetPresentation.Production.Id == entity.Production.Id))
                        {
                            var expansion = new Presentation
                            {
                                Date = presentationDate,
                                Production = entity.Production,
                                Room = entity.Room
                            };

                            if (targetPresentation == null)
                            {
                                Repository.Add(expansion, User.Identity.Name);
                            }
                            else if (inputModel.OverrideColision)
                            {
                                Repository.Delete(targetPresentation, User.Identity.Name);

                                Repository.Add(expansion, User.Identity.Name);
                            }
                        }

                        currentDate = currentDate.AddDays(1);
                    }

                    notification.Type = NotificationType.Success;
                    notification.Message = GetCreationSuccessMessage();
                }
                else
                {
                    notification.Message = GetInvalidMessage();
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
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message, null);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return RedirectToAction("calendar");
        }

        protected override string GetDefaultActionName()
        {
            return "calendar";
        }
    }
}
