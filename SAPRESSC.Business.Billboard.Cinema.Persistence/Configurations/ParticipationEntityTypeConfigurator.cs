using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class ParticipationEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Participation>
    {
        protected override string GetTableName()
        {
            return "participations";
        }

        protected override void ApplayRules(EntityTypeBuilder<Participation> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(entity => entity.Personality).WithMany();
        }
    }
}
