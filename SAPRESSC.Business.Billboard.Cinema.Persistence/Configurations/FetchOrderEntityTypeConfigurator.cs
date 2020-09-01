namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class FetchOrderEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.FetchOrder>
    {
        protected override string GetTableName()
        {
            return "fetchorders";
        }
    }
}
