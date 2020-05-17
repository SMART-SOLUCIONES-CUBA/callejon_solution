namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class FixScreenClientConfigurationEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.FixScreenClientConfiguration>
    {
        protected override string GetTableName()
        {
            return "fixscreenclientconfigurations";
        }
    }
}
