namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class ProducerHouseProductionBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.ProducerHouseProductionBridge>
    {
        protected override string GetTableName()
        {
            return "producerhouseproductionbridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ProducerHouseProductionBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ProducerHouseId, bridge.ProductionId });

            entityTypeBuilder.HasOne(bridge => bridge.ProducerHouse).WithMany(ph => ph.ProducerHouseProductionBridges).HasForeignKey(bridge => bridge.ProducerHouseId);
            entityTypeBuilder.HasOne(bridge => bridge.Production).WithMany(prod => prod.ProducerHouseProductionBridges).HasForeignKey(bridge => bridge.ProductionId);
        }
    }
}
