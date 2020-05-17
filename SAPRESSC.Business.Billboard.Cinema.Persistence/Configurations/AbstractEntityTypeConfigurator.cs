namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Configurations
{
    public abstract class AbstractEntityTypeConfigurator<TEntity> : Core.Persistence.Configurations.AbstractEntityTypeConfigurator<TEntity>
        where TEntity : class
    {
        protected override string GetSchema()
        {
            return "cinemabillboard";
        }
    }
}
