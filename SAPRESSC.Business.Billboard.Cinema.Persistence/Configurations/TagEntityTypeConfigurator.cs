namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class TagEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Tag>
    {
        protected override string GetTableName()
        {
            return "tags";
        }
    }
}
