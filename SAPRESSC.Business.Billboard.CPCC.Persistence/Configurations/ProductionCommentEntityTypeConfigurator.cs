namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class ProductionCommentEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.ProductionComment>
    {
        protected override string GetTableName()
        {
            return "productioncomments";
        }
    }
}
