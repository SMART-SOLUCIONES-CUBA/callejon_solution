using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Controllers.Management
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("cpccbillboard")]
    public class FixScreenClientConfigurationsController : Core.Web.Management.Controllers.AbstractHabilitableEntityManagementController<FixScreenClientConfiguration, Guid, FixScreenClientConfigurationInputViewModel, FixScreenClientConfigurationDisplayViewModel>
    {
        public FixScreenClientConfigurationsController(IEntityRepository<FixScreenClientConfiguration, Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<FixScreenClientConfigurationsController> localizer, ILogger<FixScreenClientConfigurationsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerCreator)]
        public override IActionResult Create(FixScreenClientConfigurationInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerEditor)]
        public override IActionResult Edit(FixScreenClientConfigurationInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerEditor)]
        public override ActionResult Enable(Guid key)
        {
            if (key == Common.Constants.DEFAULT_FIXSCREENCLIENT_CONFIGURATION_ID)
            {
                var notification = new NotificationViewModel
                {
                    Type = NotificationType.Warning,
                    Message = GetDefaultEnabledStatusNotEditableMessage()
                };

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            return base.Enable(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerEditor)]
        public override ActionResult Disable(Guid key)
        {
            if (key == Common.Constants.DEFAULT_FIXSCREENCLIENT_CONFIGURATION_ID)
            {
                var notification = new NotificationViewModel
                {
                    Type = NotificationType.Warning,
                    Message = GetDefaultEnabledStatusNotEditableMessage()
                };

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            return base.Disable(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerRemover)]
        public override IActionResult Delete(Guid key)
        {
            if (key == Common.Constants.DEFAULT_FIXSCREENCLIENT_CONFIGURATION_ID)
            {
                var notification = new NotificationViewModel
                {
                    Type = NotificationType.Warning,
                    Message = GetDefaultNotRemovableMessage()
                };

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            return base.Delete(key);
        }

        [RequiresRoleBasePermission(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerRemover)]
        public override IActionResult Delete(FixScreenClientConfigurationDisplayViewModel displayModel)
        {

            if (displayModel.Id == Common.Constants.DEFAULT_FIXSCREENCLIENT_CONFIGURATION_ID)
            {
                var notification = new NotificationViewModel
                {
                    Type = NotificationType.Warning,
                    Message = GetDefaultNotRemovableMessage()
                };

                TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

                return RedirectToAction(GetDefaultActionName());
            }

            return base.Delete(displayModel);
        }

        protected override FixScreenClientConfigurationInputViewModel ConfigureCreate(FixScreenClientConfigurationInputViewModel modelInput)
        {
            var defaultEntity = Repository.Get(Common.Constants.DEFAULT_FIXSCREENCLIENT_CONFIGURATION_ID);

            modelInput.Import(defaultEntity);

            modelInput.Id = Guid.NewGuid();

            return modelInput;
        }

        protected override void ValidateCreate(FixScreenClientConfigurationInputViewModel modelInput)
        {
            base.ValidateCreate(modelInput);

            if (Repository.FindBy(entity => entity.Id == modelInput.Id).Any())
            {
                ModelState.AddModelError("Id", "FixScreenClientConfigurationIdColisionMesssage");
            }
        }

        private string GetDefaultNotRemovableMessage()
        {
            return "DefaultNotRemovableMessage";
        }

        private string GetDefaultEnabledStatusNotEditableMessage()
        {
            return "DefaultEnabledStatusNotEditableMessage";
        }
    }
}
