namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class ProductionAudioVisualEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.ProductionAudioVisual>
    {
        protected override string GetTableName()
        {
            return "productionaudiovisuals";
        }

        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ProductionAudioVisual> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(entity => entity.Production).WithMany().HasForeignKey(e => e.ProductionId);
        }
    }
}
