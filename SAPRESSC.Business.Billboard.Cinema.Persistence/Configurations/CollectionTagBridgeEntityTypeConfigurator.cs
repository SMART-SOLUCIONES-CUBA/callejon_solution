using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class CollectionTagBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.CollectionTagBridge>
    {
        protected override string GetTableName()
        {
            return "collectiontagbridges";
        }

        protected override void ApplayRules(EntityTypeBuilder<Entities.CollectionTagBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.CollectionId, bridge.TagId });

            entityTypeBuilder.HasOne(bridge => bridge.Collection).WithMany(prod => prod.TagBridges).HasForeignKey(bridge => bridge.CollectionId);
            entityTypeBuilder.HasOne(bridge => bridge.Tag).WithMany().HasForeignKey(bridge => bridge.TagId);
        }
    }
}
