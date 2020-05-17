using Microsoft.Extensions.DependencyInjection;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Common
{
    public class CPCCBillboardBusinessCommonApplicationModule : Core.Web.Common.IApplicationModule
    {
        public Core.Persistence.Context.IDbContextDataInitialiser GetDbContextDataInitialiser()
        {
            return new Persistence.Context.DefaultDbContextDataInitialiser();
        }

        public System.Collections.Generic.ICollection<Core.Persistence.Configurations.IEntityTypeConfigurator> GetEntityTypeConfigurators()
        {
            return new System.Collections.Generic.List<Core.Persistence.Configurations.IEntityTypeConfigurator>
            {
                new AdviseEntityTypeConfigurator(),
                new FixScreenClientConfigurationEntityTypeConfigurator(),
                new EventEntityTypeConfigurator(),
                new EventPersonalityBridgeEntityTypeConfigurator(),
                new ClientEntityTypeConfigurator(),
                new EventCommentEntityTypeConfigurator(),
                new ProductionCommentEntityTypeConfigurator(),
                new PersonalityCommentEntityTypeConfigurator(),
                new EventCommentDelationEntityTypeConfigurator(),
                new ProductionCommentDelationEntityTypeConfigurator(),
                new PersonalityCommentDelationEntityTypeConfigurator(),
                new PortableDbDownloadEntityTypeConfigurator(),
                new CommentClassifierEntityTypeConfigurator()
            };
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IEntityRepository<Advise, int>, AdviseRepository>();
            services.AddScoped<IEntityRepository<FixScreenClientConfiguration, System.Guid>, FixScreenClientConfigurationRepository>();
            services.AddScoped<IEntityRepository<Event, System.Guid>, EventRepository>();
            services.AddScoped<IEntityRepository<Client, System.Guid>, ClientRepository>();
            services.AddScoped<IEntityRepository<EventComment, System.Guid>, EventCommentRepository>();
            services.AddScoped<IEntityRepository<ProductionComment, System.Guid>, ProductionCommentRepository>();
            services.AddScoped<IEntityRepository<PersonalityComment, System.Guid>, PersonalityCommentRepository>();
            services.AddScoped<IEntityRepository<EventCommentDelation, System.Guid>, EventCommentDelationRepository>();
            services.AddScoped<IEntityRepository<ProductionCommentDelation, System.Guid>, ProductionCommentDelationRepository>();
            services.AddScoped<IEntityRepository<PersonalityCommentDelation, System.Guid>, PersonalityCommentDelationRepository>();
            services.AddScoped<IEntityRepository<PortableDbDownload, long>, PortableDbDownloadRepository>();
            services.AddScoped<IEntityRepository<CommentClassifier, int>, CommentClassifierRepository>();
        }
    }
}
