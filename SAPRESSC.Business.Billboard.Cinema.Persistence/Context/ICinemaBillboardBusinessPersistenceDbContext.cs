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

        //DbSet<ProductionTagBridge> ProductionTagBridges { get; set; }

        //DbSet<Category> Categories { get; set; }

        //DbSet<Tag> Tags { get; set; }

        //DbSet<Collection> Collections { get; set; }

        //DbSet<CollectionGenreBridge> CollectionGenreBridges { get; set; }

        //DbSet<CollectionTagBridge> CollectionTagBridges { get; set; }

        //DbSet<CollectionProducerHouseBridge> CollectionProducerHouseBridges { get; set; }

        //DbSet<CollectionCheckout> CollectionCheckouts { get; set; }

        //DbSet<ProductionCheckout> ProductionCheckouts { get; set; }

        //DbSet<FetchOrder> FetchOrders { get; set; }

        //DbSet<FetchOrderCollectionBridge> FetchOrderCollectionBridges { get; set; }

        //DbSet<FetchOrderProductionBridge> FetchOrderProductionBridges { get; set; }
    }
}
