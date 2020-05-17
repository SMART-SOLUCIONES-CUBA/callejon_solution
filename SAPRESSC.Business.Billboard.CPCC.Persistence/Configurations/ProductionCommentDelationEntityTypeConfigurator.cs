namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class ProductionCommentDelationEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.ProductionCommentDelation>
    {
        protected override string GetTableName()
        {
            return "productioncommentdelations";
        }
    }
}
