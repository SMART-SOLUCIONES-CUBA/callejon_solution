namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class FetchOrderCollectionBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.FetchOrderCollectionBridge>
    {
        protected override string GetTableName()
        {
            return "fetchordercollectionbridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.FetchOrderCollectionBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.FetchOrderId, entity.CollectionId });

            entityTypeBuilder.HasOne(bridge => bridge.FetchOrder).WithMany(entity => entity.CollectionBridges).HasForeignKey(bridge => bridge.FetchOrderId);
            entityTypeBuilder.HasOne(bridge => bridge.Collection).WithMany().HasForeignKey(bridge => bridge.CollectionId);
        }
    }
}
