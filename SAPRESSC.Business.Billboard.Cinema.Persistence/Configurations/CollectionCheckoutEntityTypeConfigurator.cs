namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class CollectionCheckoutEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.CollectionCheckout>
    {
        protected override string GetTableName()
        {
            return "collectioncheckouts";
        }
    }
}
