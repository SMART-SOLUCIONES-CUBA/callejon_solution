using Microsoft.Extensions.DependencyInjection;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.SecurityLocal
{
    public class CinemaBillboardBusinessSecurityLocalApplicationModule : Core.Web.Common.IApplicationModule
    {
        public Core.Persistence.Context.IDbContextDataInitialiser GetDbContextDataInitialiser()
        {
            return new Persistence.Context.DefaultDbContextDataInitialiser();
        }

        public System.Collections.Generic.ICollection<Core.Persistence.Configurations.IEntityTypeConfigurator> GetEntityTypeConfigurators()
        {
            return null;
        }

        public void RegisterServices(IServiceCollection services)
        {
        }
    }
}
