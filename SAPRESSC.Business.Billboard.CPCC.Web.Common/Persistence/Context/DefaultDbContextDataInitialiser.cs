using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Common.Persistence.Context
{
    public class DefaultDbContextDataInitialiser : Core.Persistence.Context.IDbContextDataInitialiser
    {
        public void InitialiseData(Microsoft.EntityFrameworkCore.DbContext context)
        {
            var dbSet = context.Set<CPCC.Persistence.Entities.FixScreenClientConfiguration>();

            if (!dbSet.Any(entity => entity.Id == Constants.DEFAULT_FIXSCREENCLIENT_CONFIGURATION_ID))
            {
                dbSet.Add(new CPCC.Persistence.Entities.FixScreenClientConfiguration
                {
                    Id = Constants.DEFAULT_FIXSCREENCLIENT_CONFIGURATION_ID,
                    StartUpDelayTime = 0,
                    ShowPresentations = true,
                    ShowEvents = true,
                    DailyRoomBillboardItemMaxCount = 4,
                    DailyRoomBillboardDisplayTime = 5,
                    EventDetailDisplayTime = 20,
                    PresentationDetailDisplayTime = 20,
                    CinemaOverviewDisplayTime = 10,
                    PlaylistSidebarItemsCount = 5,
                    AdvisesChainWellcome = "Bienvenidos al complejo cultural Callejón de los Milagros, gracias por su visita",
                    AdvisesChainPlaceholder = "En breve nuevos avisos relacionados a la cultura en nuestra ciudad",
                    Description = "Configuración por defecto para las aplicaciones cliente de pantalla fija.",
                    Enabled = true
                });
            }

            if (context is Core.Persistence.Context.ITraceableDbContext)
            {
                (context as Core.Persistence.Context.ITraceableDbContext).SaveChanges("system");
            }
            else
            {
                context.SaveChanges();
            }
        }
    }
}
