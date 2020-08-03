using Microsoft.Extensions.DependencyInjection;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Common
{
    public class CinemaBillboardBusinessCommonApplicationModule : Core.Web.Common.IApplicationModule
    {
        public Core.Persistence.Context.IDbContextDataInitialiser GetDbContextDataInitialiser()
        {
            return null;
        }

        public System.Collections.Generic.ICollection<Core.Persistence.Configurations.IEntityTypeConfigurator> GetEntityTypeConfigurators()
        {
            return new System.Collections.Generic.List<Core.Persistence.Configurations.IEntityTypeConfigurator>
            {
                new AwardEntityTypeConfigurator(),
                new ClasificationEntityTypeConfigurator(),
                new AwardWinningEntityTypeConfigurator(),
                new GenreEntityTypeConfigurator(),
                new LanguageEntityTypeConfigurator(),
                new ParticipationEntityTypeConfigurator(),
                new ProducerHouseProductionBridgeEntityTypeConfigurator(),
                new ProductionEntityTypeConfigurator(),
                new ProductionGenreBridgeEntityTypeConfigurator(),
                new PresentationEntityTypeConfigurator(),
                new RoomEntityTypeConfigurator(),
                new CountryCoProductionBridgeEntityTypeConfigurator(),
                new CategoryEntityTypeConfigurator(),
                new CollectionEntityTypeConfigurator(),
                new TagEntityTypeConfigurator(),
                new ProductionTagBridgeEntityTypeConfigurator()
            };
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IEntityRepository<Award, int>, AwardRepository>();
            services.AddScoped<IEntityRepository<AwardWinning, int>, AwardWinningRepository>();
            services.AddScoped<IEntityRepository<Clasification, int>, ClasificationRepository>();
            services.AddScoped<IEntityRepository<Personality, long>, PersonalityRepository>();
            services.AddScoped<IEntityRepository<Language, int>, LanguageRepository>();
            services.AddScoped<IEntityRepository<Production, long>, ProductionRepository>();
            services.AddScoped<IEntityRepository<AwardWinning, int>, AwardWinningRepository>();
            services.AddScoped<IEntityRepository<Participation, long>, ParticipationRepository>();
            services.AddScoped<IEntityRepository<ProducerHouse, long>, ProducerHouseRepository>();
            services.AddScoped<IEntityRepository<Genre, int>, GenreRepository>();
            services.AddScoped<IEntityRepository<Room, System.Guid>, RoomRepository>();
            services.AddScoped<IEntityRepository<Persistence.Entities.Cinema, long>, CinemaRepository>();
            services.AddScoped<IEntityRepository<Presentation, long>, PresentationRepository>();
            services.AddScoped<IEntityRepository<Category, System.Guid>, CategoryRepository>();
            services.AddScoped<IEntityRepository<Tag, int>, TagRepository>();
            services.AddScoped<IEntityRepository<Collection, System.Guid>, CollectionRepository>();
        }
    }
}
