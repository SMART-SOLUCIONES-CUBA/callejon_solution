namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class AwardEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Award>
    {
        protected override string GetTableName()
        {
            return "awards";
        }
    }
}
