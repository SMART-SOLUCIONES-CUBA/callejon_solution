namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class SubscriptionGenreBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.SubscriptionGenreBridge>
    {
        protected override string GetTableName()
        {
            return "subscriptiongenrebridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.SubscriptionGenreBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.SubscriptionId, entity.GenreId });

            entityTypeBuilder.HasOne(bridge => bridge.Subscription).WithMany(entity => entity.PreferedGenreBridges).HasForeignKey(bridge => bridge.SubscriptionId);
            entityTypeBuilder.HasOne(bridge => bridge.Genre).WithMany().HasForeignKey(bridge => bridge.GenreId);
        }
    }
}
