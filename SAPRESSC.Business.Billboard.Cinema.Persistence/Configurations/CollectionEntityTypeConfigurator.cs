namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class CollectionEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Collection>
    {
        protected override string GetTableName()
        {
            return "collections";
        }
    }
}
