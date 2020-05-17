namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class AdviseEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.Advise>
    {
        protected override string GetTableName()
        {
            return "advises";
        }
    }
}
