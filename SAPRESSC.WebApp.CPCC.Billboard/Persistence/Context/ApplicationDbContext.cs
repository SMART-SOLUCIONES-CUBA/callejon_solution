using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Configurations;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Persistence.Context
{
    public class ApplicationDbContext : Core.Persistence.Context.AbstractTraceableDbContext,
        Core.Security.Persistence.Context.ISecurityPersistenceModuleDbContext,
        Business.Billboard.Cinema.Persistence.Context.ICinemaBillboardBusinessPersistenceDbContext,
        Business.Billboard.CPCC.Persistence.Context.ICPCCBillboardBusinessPersistenceDbContext
    {
        private readonly IApplicationContext applicationContext;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IApplicationContext applicationContext) : base(options)
        {
            this.applicationContext = applicationContext;
        }

        public DbSet<CommentClassifier> CommentClassifiers { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroupBridge> UserGroupBridges { get; set; }
        public DbSet<GroupRoleBridge> GroupRoleBridges { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<CountryRegion> CountryRegions { get; set; }
        public DbSet<StateRegion> StateRegions { get; set; }
        public DbSet<MunicipalityRegion> MunicipalityRegions { get; set; }
        public DbSet<LocalityRegion> LocalityRegions { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<EmailContactItem> EmailContactItems { get; set; }
        public DbSet<PhoneContactItem> PhoneContactItems { get; set; }
        public DbSet<SocialContactItem> SocialContactItems { get; set; }
        public DbSet<WebSiteContactItem> WebSiteContactItems { get; set; }
        public DbSet<PostalContactItem> PostalContactItems { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Personality> Personalities { get; set; }
        public DbSet<PersonEmailContactItemBridge> PersonEmailContactItemBridges { get; set; }
        public DbSet<PersonPhoneContactItemBridge> PersonPhoneContactItemBridges { get; set; }
        public DbSet<PersonPostalContactItemBridge> PersonPostalContactItemBridges { get; set; }
        public DbSet<PersonSocialContactItemBridge> PersonSocialContactItemBridges { get; set; }
        public DbSet<PersonWebSiteContactItemBridge> PersonWebSiteContactItemBridges { get; set; }
        public DbSet<OrganizationEmailContactItemBridge> OrganizationEmailContactItemBridges { get; set; }
        public DbSet<OrganizationPhoneContactItemBridge> OrganizationPhoneContactItemBridges { get; set; }
        public DbSet<OrganizationPostalContactItemBridge> OrganizationPostalContactItemBridges { get; set; }
        public DbSet<OrganizationSocialContactItemBridge> OrganizationSocialContactItemBridges { get; set; }
        public DbSet<OrganizationWebSiteContactItemBridge> OrganizationWebSiteContactItemBridges { get; set; }

        public DbSet<Clasification> Clasifications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<AwardWinning> AwardWinnings { get; set; }
        public DbSet<ProducerHouse> ProducerHouses { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<ProductionGenreBridge> ProductionGenreBridges { get; set; }
        public DbSet<ProductionTagBridge> ProductionTagBridges { get; set; }
        public DbSet<ProducerHouseProductionBridge> ProducerHouseProductionBridges { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<CountryCoProductionBridge> CountryCoProductionBridges { get; set; }
        public DbSet<Advise> Advices { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<FixScreenClientConfiguration> FixScreenClientConfigurations { get; set; }
        public DbSet<EventPersonalityBridge> EventPersonalityBridges { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<EventComment> EventComments { get; set; }
        public DbSet<ProductionComment> ProductionComments { get; set; }
        public DbSet<PersonalityComment> PersonalityComments { get; set; }
        public DbSet<EventCommentDelation> EventCommentDelations { get; set; }
        public DbSet<ProductionCommentDelation> ProductionCommentDelations { get; set; }
        public DbSet<PersonalityCommentDelation> PersonalityCommentDelations { get; set; }
        public DbSet<CollectionGenreBridge> CollectionGenreBridges { get; set; }
        public DbSet<CollectionTagBridge> CollectionTagBridges { get; set; }
        public DbSet<CollectionProducerHouseBridge> CollectionProducerHouseBridges { get; set; }
        public DbSet<PortableDbDownload> PortableDbDownloads { get; set; }
        public DbSet<CollectionCheckout> CollectionCheckouts { get; set; }
        public DbSet<ProductionCheckout> ProductionCheckouts { get; set; }
        public DbSet<FetchOrder> FetchOrders { get; set; }
        public DbSet<FetchOrderCollectionBridge> FetchOrderCollectionBridges { get; set; }
        public DbSet<FetchOrderProductionBridge> FetchOrderProductionBridges { get; set; }
        public DbSet<Search> Searches { get; set; }
        public DbSet<SearchGenreBridge> SearchGenreBridges { get; set; }
        public DbSet<SearchPersonalityBridge> SearchPersonalityBridges { get; set; }
        public DbSet<SearchTagBridge> SearchTagBridges { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionGenreBridge> SubscriptionGenreBridges { get; set; }
        public DbSet<SubscriptionProducerHouseBridge> SubscriptionProducerHouses { get; set; }
        public DbSet<SubscriptionTagBridge> SubscriptionTagBridges { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityTypeConfigurators = new List<IEntityTypeConfigurator>();

            foreach (var webModule in applicationContext.GetWebModules())
            {
                if (webModule.GetEntityTypeConfigurators() == null) continue;

                entityTypeConfigurators.AddRange(webModule.GetEntityTypeConfigurators());
            }

            foreach (var configurator in entityTypeConfigurators)
            {
                configurator.Configure(modelBuilder, Database.GetDbConnection().Database);
            }
        }
    }

}
