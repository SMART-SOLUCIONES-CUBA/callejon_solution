using System.Configuration;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Providers
{
    public class ConfigurationProvider
    {
        public static string LibVlcPath
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("LibVclPath");
            }
        }
    }
}
