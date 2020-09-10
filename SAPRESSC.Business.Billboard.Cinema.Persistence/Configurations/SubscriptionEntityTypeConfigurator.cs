namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class SubscriptionEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Subscription>
    {
        protected override string GetTableName()
        {
            return "subscriptions";
        }
    }
}
