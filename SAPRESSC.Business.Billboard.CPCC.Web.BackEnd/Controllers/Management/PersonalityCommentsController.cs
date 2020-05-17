using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.Filter;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cpccbillboard")]
    public class PersonalityCommentsController : Core.Web.Management.Controllers.AbstractEntityManagementFilteredController<PersonalityComment, System.Guid, PersonalityCommentInputViewModel, PersonalityCommentDisplayViewModel, PersonalityCommentFilterModel>
    {
        private readonly IEntityRepository<EventCommentDelation, System.Guid> personalityCommentDelationRepository;

        public PersonalityCommentsController(IEntityRepository<EventCommentDelation, System.Guid> personalityCommentDelationRepository, IEntityRepository<PersonalityComment, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PersonalityCommentsController> localizer, ILogger<PersonalityCommentsController> logger) : base(repository, localizer, logger)
        {
            this.personalityCommentDelationRepository = personalityCommentDelationRepository;
        }

        protected override string GetFilterSessionID()
        {
            return "cpccbillboard-personalitycomments-management-filter";
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerViewer)]
        public override IActionResult Filter(PersonalityCommentFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerRemover)]
        public override IActionResult Delete(PersonalityCommentDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        public override IActionResult Create()
        {
            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(new NotificationViewModel
            {
                Message = "La funcinalidad solicitada no disponible.",
                Type = NotificationType.Warning
            });

            return RedirectToAction(GetDefaultActionName());
        }

        public override IActionResult Create(PersonalityCommentInputViewModel modelInput)
        {
            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(new NotificationViewModel
            {
                Message = "La funcinalidad solicitada no disponible.",
                Type = NotificationType.Warning
            });

            return RedirectToAction(GetDefaultActionName());
        }

        public override IActionResult Edit(System.Guid key)
        {
            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(new NotificationViewModel
            {
                Message = "La funcinalidad solicitada no disponible.",
                Type = NotificationType.Warning
            });

            return RedirectToAction(GetDefaultActionName());
        }

        public override IActionResult Edit(PersonalityCommentInputViewModel modelInput)
        {
            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(new NotificationViewModel
            {
                Message = "La funcinalidad solicitada no disponible.",
                Type = NotificationType.Warning
            });

            return RedirectToAction(GetDefaultActionName());
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerEditor)]
        public virtual ActionResult Block(System.Guid key)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            try
            {
                var entity = Repository.Get(key);

                entity.Banish = true;

                Repository.Update(entity, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetEnableSuccessMessage();
            }
            catch (System.Exception e)
            {
                logger.LogError(1, e.InnerException, e.Message, null);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

            return RedirectToAction(GetDefaultActionName());
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerEditor)]
        public virtual ActionResult UnBlock(System.Guid key)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            try
            {
                var entity = Repository.Get(key);

                entity.Banish = false;

                Repository.Update(entity, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetDisableSuccessMessage();
            }
            catch (System.Exception e)
            {
                logger.LogError(1, e.InnerException, e.Message, null);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

            return RedirectToAction(GetDefaultActionName());
        }

        protected virtual string GetEnableSuccessMessage()
        {
            return "Comentario bloqueado con éxito.";
        }

        protected virtual string GetDisableSuccessMessage()
        {
            return "Comentario desbloqueado con éxito.";
        }

        #region PersonalityComment Delations Management

        [Route("{key}/[action]")]
        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerEditor)]
        public IActionResult Delations(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var commentDisplay = new PersonalityCommentDisplayViewModel();

                commentDisplay.Import(entity);

                var model = new PersonalityCommentDelationsViewModel
                {
                    PersonalityCommet = commentDisplay,
                    Delations = entity.Delations.Select(delation =>
                    {
                        var delationDisplay = new PersonalityCommentDelationDisplayViewModel();

                        delationDisplay.Import(delation);

                        return delationDisplay;
                    }).ToArray()
                };

                return View(model);
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

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

            return RedirectToAction(GetDefaultActionName());
        }

        [Route("edit/delation/delete")]
        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDelation(PersonalityCommentDelationDisplayViewModel modelDisplay)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            try
            {
                var entity = personalityCommentDelationRepository.Get(modelDisplay.Id);

                personalityCommentDelationRepository.Delete(entity, User.Identity.Name);

                notification.Type = NotificationType.Success;
                notification.Message = GetDeletionSuccessMessage();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                notification.Type = NotificationType.Warning;
                notification.Message = GetOnDeletionDependecyConstrainErrorMessage();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

            return RedirectToAction("delations", new { key = modelDisplay.CommentId });
        }

        #endregion
    }
}
