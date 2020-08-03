using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class ProductionTagBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.ProductionTagBridge>
    {
        protected override string GetTableName()
        {
            return "productiontagbridges";
        }

        protected override void ApplayRules(EntityTypeBuilder<Entities.ProductionTagBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ProductionId, bridge.TagId });

            entityTypeBuilder.HasOne(bridge => bridge.Production).WithMany(entity => entity.TagBridges).HasForeignKey(bridge => bridge.ProductionId);
            entityTypeBuilder.HasOne(bridge => bridge.Tag).WithMany().HasForeignKey(bridge => bridge.TagId);
        }
    }
}
