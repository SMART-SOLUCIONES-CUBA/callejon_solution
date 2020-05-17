using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class CountryCoProductionBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.CountryCoProductionBridge>
    {
        protected override string GetTableName()
        {
            return "countrycoproductionbridges";
        }

        protected override void ApplayRules(EntityTypeBuilder<CountryCoProductionBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ProductionId, bridge.CountryId });

            entityTypeBuilder.HasOne(bridge => bridge.Production).WithMany(prod => prod.CountryCoProductionBridges).HasForeignKey(bridge => bridge.ProductionId);
            entityTypeBuilder.HasOne(bridge => bridge.Country).WithMany().HasForeignKey(bridge => bridge.CountryId);
        }
    }
}
