namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class AwardWinningEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.AwardWinning>
    {
        protected override string GetTableName()
        {
            return "awardwinnings";
        }
    }
}
