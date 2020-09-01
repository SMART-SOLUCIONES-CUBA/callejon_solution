namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class CollectionProducerHouseBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.CollectionProducerHouseBridge>
    {
        protected override string GetTableName()
        {
            return "collectionproducerhousebridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.CollectionProducerHouseBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.CollectionId, bridge.ProducerHouseId });

            entityTypeBuilder.HasOne(bridge => bridge.Collection).WithMany(prod => prod.ProducerHouseBridges).HasForeignKey(bridge => bridge.CollectionId);
            entityTypeBuilder.HasOne(bridge => bridge.ProducerHouse).WithMany().HasForeignKey(bridge => bridge.ProducerHouseId);
        }
    }
}
