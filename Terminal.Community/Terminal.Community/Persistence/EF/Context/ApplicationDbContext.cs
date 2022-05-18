using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Persistence.EF.Configurators;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Persistence.EF.Context
{
    public class ApplicationDbContext : 
        SAPRESSC.Core.Persistence.Context.AbstractTraceableDbContext,
        SAPRESSC.Business.Billboard.Cinema.Persistence.Context.ICinemaBillboardBusinessPersistenceDbContext,
        SAPRESSC.Business.Billboard.CPCC.Persistence.Context.ICPCCBillboardBusinessPersistenceDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Clasification> Clasifications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Personality> Personalities { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<AwardWinning> AwardWinnings { get; set; }
        public DbSet<ProducerHouse> ProducerHouses { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<ProductionGenreBridge> ProductionGenreBridges { get; set; }
        public DbSet<ProducerHouseProductionBridge> ProducerHouseProductionBridges { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<EmailContactItem> EmailContactItems { get; set; }
        public DbSet<PhoneContactItem> PhoneContactItems { get; set; }
        public DbSet<SocialContactItem> SocialContactItems { get; set; }
        public DbSet<WebSiteContactItem> WebSiteContactItems { get; set; }
        public DbSet<PostalContactItem> PostalContactItems { get; set; }
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
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<CountryRegion> CountryRegions { get; set; }
        public DbSet<StateRegion> StateRegions { get; set; }
        public DbSet<MunicipalityRegion> MunicipalityRegions { get; set; }
        public DbSet<LocalityRegion> LocalityRegions { get; set; }
        public DbSet<Advise> Advices { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<FixScreenClientConfiguration> FixScreenClientConfigurations { get; set; }
        public DbSet<EventPersonalityBridge> EventPersonalityBridges { get; set; }
        public DbSet<CountryCoProductionBridge> CountryCoProductionBridges { get; set; }
        public DbSet<CollectionGenreBridge> CollectionGenreBridges { get; set; }
        public DbSet<CollectionTagBridge> CollectionTagBridges { get; set; }
        public DbSet<CollectionProducerHouseBridge> CollectionProducerHouseBridges { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<EventComment> EventComments { get; set; }
        public DbSet<ProductionComment> ProductionComments { get; set; }
        public DbSet<PersonalityComment> PersonalityComments { get; set; }
        public DbSet<EventCommentDelation> EventCommentDelations { get; set; }
        public DbSet<ProductionCommentDelation> ProductionCommentDelations { get; set; }
        public DbSet<PersonalityCommentDelation> PersonalityCommentDelations { get; set; }
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
        public DbSet<ProductionAudioVisual> ProductionAudioVisuals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityTypeConfigurators = new System.Collections.Generic.List<SAPRESSC.Core.Persistence.Configurations.IEntityTypeConfigurator>
            {
                new ImageEntityTypeConfigurator(),
                new ThumbnailEntityTypeConfigurator(),
                new VideoEntityTypeConfigurator(),
                new CountryEntityTypeConfigurator(),
                new CountryRegionEntityTypeConfigurator(),
                new LocalityEntityTypeConfigurator(),
                new LocalityRegionEntityTypeConfigurator(),
                new MunicipalityEntityTypeConfigurator(),
                new MunicipalityRegionEntityTypeConfigurator(),
                new StateEntityTypeConfigurator(),
                new StateRegionEntityTypeConfigurator(),
                new EmailContactItemEntityTypeConfigurator(),
                new MembershipEntityTypeConfigurator(),
                new OrganizationEmailContactItemBridgeEntityTypeConfigurator(),
                new OrganizationEntityTypeConfigurator(),
                new OrganizationPhoneContactItemBridgeEntityTypeConfigurator(),
                new OrganizationPostalContactItemBridgeEntityTypeConfigurator(),
                new OrganizationSocialContactItemBridgeEntityTypeConfigurator(),
                new OrganizationWebSiteContactItemBridgeEntityTypeConfigurator(),
                new PersonEmailContactItemBridgeEntityTypeConfigurator(),
                new PersonEntityTypeConfigurator(),
                new PersonPhoneContactItemBridgeEntityTypeConfigurator(),
                new PersonPostalContactItemBridgeEntityTypeConfigurator(),
                new PersonSocialContactItemBridgeEntityTypeConfigurator(),
                new PersonWebSiteContactItemBridgeEntityTypeConfigurator(),
                new PhoneContactItemEntityTypeConfigurator(),
                new PostalContactItemEntityTypeConfigurator(),
                new SocialContactItemEntityTypeConfigurator(),
                new WebSiteContactItemEntityTypeConfigurator(),
                new AdvertisementEntityTypeConfigurator(),
                new AwardEntityTypeConfigurator(),
                new AwardWinningEntityTypeConfigurator(),
                new CategoryEntityTypeConfigurator(),
                new ClasificationEntityTypeConfigurator(),
                new CollectionCheckoutEntityTypeConfigurator(),
                new CollectionEntityTypeConfigurator(),
                new CollectionGenreBridgeEntityTypeConfigurator(),
                new CollectionProducerHouseBridgeEntityTypeConfigurator(),
                new CollectionTagBridgeEntityTypeConfigurator(),
                new FetchOrderCollectionBridgeEntityTypeConfigurator(),
                new FetchOrderEntityTypeConfigurator(),
                new FetchOrderProductionBridgeEntityTypeConfigurator(),
                new GenreEntityTypeConfigurator(),
                new LanguageEntityTypeConfigurator(),
                new ParticipationEntityTypeConfigurator(),
                new PresentationEntityTypeConfigurator(),
                new ProducerHouseProductionBridgeEntityTypeConfigurator(),
                new ProductionCheckoutEntityTypeConfigurator(),
                new ProductionEntityTypeConfigurator(),
                new ProductionGenreBridgeEntityTypeConfigurator(),
                new ProductionTagBridgeEntityTypeConfigurator(),
                new RoomEntityTypeConfigurator(),
                new SearchEntityTypeConfigurator(),
                new SearchGenreBridgeEntityTypeConfigurator(),
                new SearchPersonalityBridgeEntityTypeConfigurator(),
                new SearchTagBridgeEntityTypeConfigurator(),
                new SubscriptionEntityTypeConfigurator(),
                new SubscriptionGenreBridgeEntityTypeConfigurator(),
                new SubscriptionProducerHouseBridgeEntityTypeConfigurator(),
                new SubscriptionTagBridgeEntityTypeConfigurator(),
                new TagEntityTypeConfigurator(),
                new AdviseEntityTypeConfigurator(),
                new EventEntityTypeConfigurator(),
                new EventPersonalityBridgeEntityTypeConfigurator(),
                new FixScreenClientConfigurationEntityTypeConfigurator(),
                new CountryCoProductionBridgeEntityTypeConfigurator(),
                new ClientEntityTypeConfigurator(),
                new EventCommentEntityTypeConfigurator(),
                new PersonalityCommentEntityTypeConfigurator(),
                new ProductionCommentEntityTypeConfigurator(),
                new EventCommentDelationEntityTypeConfigurator(),
                new PersonalityCommentDelationEntityTypeConfigurator(),
                new ProducerHouseProductionBridgeEntityTypeConfigurator(),
                new PortableDbDownloadEntityTypeConfigurator(),
                new ProductionAudioVisualEntityTypeConfigurator()
            };

            foreach (var configurator in entityTypeConfigurators)
            {
                configurator.Configure(modelBuilder, Database.GetDbConnection().Database);
            }
        }
    }
}
