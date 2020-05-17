namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class LanguageEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Language>
    {
        protected override string GetTableName()
        {
            return "languages";
        }
    }
}
