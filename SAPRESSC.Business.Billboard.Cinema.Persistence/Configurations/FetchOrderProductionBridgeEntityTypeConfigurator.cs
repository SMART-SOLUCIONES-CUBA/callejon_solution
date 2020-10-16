namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class FetchOrderProductionBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.FetchOrderProductionBridge>
    {
        protected override string GetTableName()
        {
            return "fetchorderproductionbridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.FetchOrderProductionBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.FetchOrderId, entity.ProductionId });

            entityTypeBuilder.HasOne(bridge => bridge.FetchOrder).WithMany(entity => entity.ProductionBridges).HasForeignKey(bridge => bridge.FetchOrderId);
            entityTypeBuilder.HasOne(bridge => bridge.Production).WithMany(entity => entity.FetchOrders).HasForeignKey(bridge => bridge.ProductionId);
        }
    }
}
