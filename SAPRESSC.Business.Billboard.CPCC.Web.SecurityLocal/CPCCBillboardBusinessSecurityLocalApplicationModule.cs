using Microsoft.Extensions.DependencyInjection;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.SecurityLocal
{
    public class CPCCBillboardBusinessSecurityLocalApplicationModule : Core.Web.Common.IApplicationModule
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
