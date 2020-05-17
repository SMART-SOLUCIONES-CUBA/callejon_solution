namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class GenreEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Genre>
    {
        protected override string GetTableName()
        {
            return "genres";
        }
    }
}
