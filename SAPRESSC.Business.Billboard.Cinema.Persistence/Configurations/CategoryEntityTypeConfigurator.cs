namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class CategoryEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Category>
    {
        protected override string GetTableName()
        {
            return "categories";
        }
    }
}
