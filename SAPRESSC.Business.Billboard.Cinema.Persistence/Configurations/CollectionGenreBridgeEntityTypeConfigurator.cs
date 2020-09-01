using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class CollectionGenreBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.CollectionGenreBridge>
    {
        protected override string GetTableName()
        {
            return "collectiongenrebridges";
        }

        protected override void ApplayRules(EntityTypeBuilder<Entities.CollectionGenreBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.CollectionId, bridge.GenreId });

            entityTypeBuilder.HasOne(bridge => bridge.Collection).WithMany(prod => prod.GenreBridges).HasForeignKey(bridge => bridge.CollectionId);
            entityTypeBuilder.HasOne(bridge => bridge.Genre).WithMany(genre => genre.CollectionBridges).HasForeignKey(bridge => bridge.GenreId);
        }
    }
}
