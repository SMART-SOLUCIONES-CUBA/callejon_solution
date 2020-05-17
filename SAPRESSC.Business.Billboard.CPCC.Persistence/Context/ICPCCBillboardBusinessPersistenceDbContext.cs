using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Context
{
    public interface ICPCCBillboardBusinessPersistenceDbContext
    {
        DbSet<Advise> Advices { get; set; }

        DbSet<Event> Events { get; set; }

        DbSet<FixScreenClientConfiguration> FixScreenClientConfigurations { get; set; }

        DbSet<EventPersonalityBridge> EventPersonalityBridges { get; set; }

        DbSet<Client> Clients { get; set; }

        DbSet<EventComment> EventComments { get; set; }

        DbSet<ProductionComment> ProductionComments { get; set; }

        DbSet<PersonalityComment> PersonalityComments { get; set; }

        DbSet<EventCommentDelation> EventCommentDelations { get; set; }

        DbSet<ProductionCommentDelation> ProductionCommentDelations { get; set; }

        DbSet<PersonalityCommentDelation> PersonalityCommentDelations { get; set; }

        DbSet<PortableDbDownload> PortableDbDownloads { get; set; }
    }
}
