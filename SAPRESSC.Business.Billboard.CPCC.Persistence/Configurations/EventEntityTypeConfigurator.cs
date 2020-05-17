namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class EventEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Event>
    {
        protected override string GetTableName()
        {
            return "events";
        }
    }
}
