using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class ProductionGenreBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.ProductionGenreBridge>
    {
        protected override string GetTableName()
        {
            return "productiongenrebridges";
        }

        protected override void ApplayRules(EntityTypeBuilder<Entities.ProductionGenreBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ProductionId, bridge.GenreId });

            entityTypeBuilder.HasOne(bridge => bridge.Production).WithMany(prod => prod.ProductionGenreBridges).HasForeignKey(bridge => bridge.ProductionId);
            entityTypeBuilder.HasOne(bridge => bridge.Genre).WithMany(genre => genre.ProductionGenreBridges).HasForeignKey(bridge => bridge.GenreId);
        }
    }
}
