namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class SearchPersonalityBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.SearchPersonalityBridge>
    {
        protected override string GetTableName()
        {
            return "searchpersonalitybridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.SearchPersonalityBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.SearchId, entity.PersonalityId });

            entityTypeBuilder.HasOne(bridge => bridge.Search).WithMany(entity => entity.PersonalityBridges).HasForeignKey(bridge => bridge.SearchId);
            entityTypeBuilder.HasOne(bridge => bridge.Personality).WithMany().HasForeignKey(bridge => bridge.PersonalityId);
        }
    }
}
