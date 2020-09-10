namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class SearchTagBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.SearchTagBridge>
    {
        protected override string GetTableName()
        {
            return "searchtagbridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.SearchTagBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.SearchId, entity.TagId });

            entityTypeBuilder.HasOne(bridge => bridge.Search).WithMany(entity => entity.TagBridges).HasForeignKey(bridge => bridge.SearchId);
            entityTypeBuilder.HasOne(bridge => bridge.Tag).WithMany().HasForeignKey(bridge => bridge.TagId);
        }
    }
}
