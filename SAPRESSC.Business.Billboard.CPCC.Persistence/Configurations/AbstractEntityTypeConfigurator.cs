namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public abstract class AbstractEntityTypeConfigurator<TEntity> : Core.Persistence.Configurations.AbstractEntityTypeConfigurator<TEntity>
        where TEntity : class
    {
        protected override string GetSchema()
        {
            return "cpccbillboard";
        }
    }
}
