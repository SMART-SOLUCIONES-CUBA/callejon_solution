namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class PresentationEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Presentation>
    {
        protected override string GetTableName()
        {
            return "presentations";
        }
    }
}
