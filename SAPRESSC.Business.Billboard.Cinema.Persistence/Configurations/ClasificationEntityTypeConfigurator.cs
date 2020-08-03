namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class ClasificationEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Clasification>
    {
        protected override string GetTableName()
        {
            return "clasifications";
        }
    }
}
