namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class EventPersonalityBridgeEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.EventPersonalityBridge>
    {
        protected override string GetTableName()
        {
            return "eventoersonalitybridges";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.EventPersonalityBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.EventId, bridge.PersonalityId });

            entityTypeBuilder.HasOne(bridge => bridge.Event).WithMany(entity => entity.EventPersonalityBridges).HasForeignKey(bridge => bridge.EventId);
            entityTypeBuilder.HasOne(bridge => bridge.Personality).WithMany().HasForeignKey(bridge => bridge.PersonalityId);
        }
    }
}
