namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class SearchEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Search>
    {
        protected override string GetTableName()
        {
            return "searchs";
        }
    }
}
