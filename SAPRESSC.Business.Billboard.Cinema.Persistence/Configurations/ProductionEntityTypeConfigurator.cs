namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class ProductionEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Production>
    {
        protected override string GetTableName()
        {
            return "productions";
        }
    }
}
