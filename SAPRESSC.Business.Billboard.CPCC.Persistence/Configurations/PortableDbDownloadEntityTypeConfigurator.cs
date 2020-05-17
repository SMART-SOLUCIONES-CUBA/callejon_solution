namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class PortableDbDownloadEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.PortableDbDownload>
    {
        protected override string GetTableName()
        {
            return "portabledbdownloads";
        }
    }
}
