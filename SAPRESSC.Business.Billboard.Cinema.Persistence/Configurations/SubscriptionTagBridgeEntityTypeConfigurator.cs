namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class SubscriptionTagBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.SubscriptionTagBridge>
    {
        protected override string GetTableName()
        {
            return "subscriptiontagbridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.SubscriptionTagBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.SubscriptionId, entity.TagId });

            entityTypeBuilder.HasOne(bridge => bridge.Subscription).WithMany(entity => entity.PreferedTagBridges).HasForeignKey(bridge => bridge.SubscriptionId);
            entityTypeBuilder.HasOne(bridge => bridge.Tag).WithMany().HasForeignKey(bridge => bridge.TagId);
        }
    }
}
