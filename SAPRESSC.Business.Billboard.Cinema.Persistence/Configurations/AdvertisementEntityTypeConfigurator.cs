namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class AdvertisementEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Advertisement>
    {
        protected override string GetTableName()
        {
            return "advertisements";
        }
    }
}
