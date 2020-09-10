using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Context
{
    public interface ICinemaBillboardBusinessPersistenceDbContext : Middleware.Identity.Persistence.Context.IIdentityMiddlewarePersistenceDbContext
    {
        DbSet<Clasification> Clasifications { get; set; }

        DbSet<Personality> Personalities { get; set; }

        DbSet<Genre> Genres { get; set; }

        DbSet<Award> Awards { get; set; }

        DbSet<Language> Languages { get; set; }

        DbSet<AwardWinning> AwardWinnings { get; set; }

        DbSet<ProducerHouse> ProducerHouses { get; set; }

        DbSet<Participation> Participations { get; set; }

        DbSet<Entities.Cinema> Cinemas { get; set; }

        DbSet<Room> Rooms { get; set; }

        DbSet<Production> Productions { get; set; }

        DbSet<ProductionGenreBridge> ProductionGenreBridges { get; set; }

        DbSet<ProducerHouseProductionBridge> ProducerHouseProductionBridges { get; set; }

        DbSet<Presentation> Presentations { get; set; }

        DbSet<CountryCoProductionBridge> CountryCoProductionBridges { get; set; }
    }
}
