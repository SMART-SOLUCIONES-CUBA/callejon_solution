namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public class ProductionCheckoutEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.ProductionCheckout>
    {
        protected override string GetTableName()
        {
            return "productioncheckouts";
        }
    }
}
