namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class SearchGenreBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.SearchGenreBridge>
    {
        protected override string GetTableName()
        {
            return "searchgenrebridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.SearchGenreBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(entity => new { entity.SearchId, entity.GenreId });

            entityTypeBuilder.HasOne(bridge => bridge.Search).WithMany(entity => entity.GenreBridges).HasForeignKey(bridge => bridge.SearchId);
            entityTypeBuilder.HasOne(bridge => bridge.Genre).WithMany().HasForeignKey(bridge => bridge.GenreId);
        }
    }
}
