[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Providers.NetworkInformationProvider))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Providers
{
    public class NetworkInformationProvider : Community.Providers.Facade.INetworkInformationProviderFacade
    {
        public System.Net.IPAddress GetBreadcastIPAddress()
        {
            return System.Net.IPAddress.Parse("192.168.43.255");

            //var wifiManager = (Android.Net.Wifi.WifiManager)Xamarin.Essentials.Platform.CurrentActivity.GetSystemService(Android.Content.Context.WifiService);

            //var dhcpInfo = wifiManager.DhcpInfo;

            //var broadcastRange = (dhcpInfo.IpAddress & dhcpInfo.Netmask) | ~dhcpInfo.Netmask;

            //var broadcastQuads = new byte[4];

            //for (int i = 0; i < 4; i++)
            //{
            //    broadcastQuads[i] = (byte)(broadcastRange >> (i * 8));
            //}

            //return System.Net.IPAddress.Parse(Java.Net.InetAddress.GetByAddress(broadcastQuads).ToString());
        }
    }
}