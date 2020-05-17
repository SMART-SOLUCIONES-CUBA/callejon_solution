namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class ClientEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Client>
    {
        protected override string GetTableName()
        {
            return "client";
        }
    }
}
