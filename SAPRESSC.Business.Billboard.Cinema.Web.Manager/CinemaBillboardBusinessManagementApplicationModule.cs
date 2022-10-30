using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Configurations;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Context;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Models.View;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager
{
    public class CinemaBillboardBusinessManagementApplicationModule : Core.Web.Management.IManagementApplicationModule
    {
        public ICollection<DashboardManageItemViewModel> GetDashboardManagementItems()
        {
            return new List<DashboardManageItemViewModel>
            {
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cinemabillboard",
                    Controller = "advertisements",
                    Role = CinemaBillboardBusinessManagementRoleCodes.AdvertisementManagerViewer,
                    Title = "Anuncios"
                },
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cinemabillboard",
                    Controller = "categories",
                    Role = CinemaBillboardBusinessManagementRoleCodes.CategoryManagerViewer,
                    Title = "Categories"
                },
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cinemabillboard",
                    Controller = "clasifications",
                    Role = CinemaBillboardBusinessManagementRoleCodes.ClasificationManagerViewer,
                    Title = "Clasifications"
                },
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cinemabillboard",
                    Controller = "personalities",
                    Role = CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerViewer,
                    Title = "Personalities"
                },
                new DashboardManageItemViewModel
                {
                    Title = "Genres",
                    Action = "index",
                    Controller = "genres",
                    Area = "cinemabillboard",
                    Role = CinemaBillboardBusinessManagementRoleCodes.GenreManagerViewer
                },
                new DashboardManageItemViewModel
                {
                    Title = "Awards",
                    Action = "index",
                    Controller = "awards",
                    Area = "cinemabillboard",
                    Role = CinemaBillboardBusinessManagementRoleCodes.AwardManagerViewer
                },
                new DashboardManageItemViewModel
                {
                    Title = "Languages",
                    Action = "index",
                    Controller = "languages",
                    Area = "cinemabillboard",
                    Role = CinemaBillboardBusinessManagementRoleCodes.LanguageManagerViewer
                },
                new DashboardManageItemViewModel
                {
                    Title = "ProducerHouses",
                    Action = "index",
                    Controller = "producerhouses",
                    Area = "cinemabillboard",
                    Role = CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerViewer
                },
                new DashboardManageItemViewModel
                {
                    Title = "Productions",
                    Action = "index",
                    Controller = "productions",
                    Area = "cinemabillboard",
                    Role = CinemaBillboardBusinessManagementRoleCodes.ProductionManagerViewer
                },
                new DashboardManageItemViewModel
                {
                    Title = "Cinemas",
                    Action = "index",
                    Controller = "cinemas",
                    Area = "cinemabillboard",
                    Role = CinemaBillboardBusinessManagementRoleCodes.CinemaManagerViewer
                },
                new DashboardManageItemViewModel
                {
                    Title = "Presentations",
                    Action = "calendar",
                    Controller = "presentations",
                    Area = "cinemabillboard",
                    Role = CinemaBillboardBusinessManagementRoleCodes.PresentationManagerViewer
                },
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cinemabillboard",
                    Controller = "collections",
                    Role = CinemaBillboardBusinessManagementRoleCodes.CollectionManagerViewer,
                    Title = "Collections"
                },
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "cinemabillboard",
                    Controller = "tags",
                    Role = CinemaBillboardBusinessManagementRoleCodes.TagManagerViewer,
                    Title = "Tags"
                },
                new DashboardManageItemViewModel
                {
                    Action = "index",
                    Area = "mediastore",
                    Controller = "subscriptions",
                    Role = CinemaBillboardBusinessManagementRoleCodes.SubscriptioManagerViewer,
                    Title = "Suscripciones"
                }
            };
        }

        public IDbContextDataInitialiser GetDbContextDataInitialiser()
        {
            return null;
        }

        public ICollection<IEntityTypeConfigurator> GetEntityTypeConfigurators()
        {
            return null;
        }

        public void RegisterServices(IServiceCollection services)
        {
        }
    }
}
