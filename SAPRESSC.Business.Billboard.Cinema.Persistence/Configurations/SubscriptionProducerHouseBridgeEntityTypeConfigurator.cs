namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class SubscriptionProducerHouseBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.SubscriptionProducerHouseBridge>
    {
        protected override string GetTableName()
        {
            return "subscriptionproducerhousebridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.SubscriptionProducerHouseBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.SubscriptionId, entity.ProducerHouseId });

            entityTypeBuilder.HasOne(bridge => bridge.Subscription).WithMany(entity => entity.PreferedProducerHouseBridges).HasForeignKey(bridge => bridge.SubscriptionId);
            entityTypeBuilder.HasOne(bridge => bridge.ProducerHouse).WithMany().HasForeignKey(bridge => bridge.ProducerHouseId);
        }
    }
}
