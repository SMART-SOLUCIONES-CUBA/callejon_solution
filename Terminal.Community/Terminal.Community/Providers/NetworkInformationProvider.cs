using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Providers
{
    public static class NetworkInformationProvider
    {
        public static Models.NetworkInformationModel GetNetworkInformation()
        {
            Models.NetworkInformationModel result = null;

            var targetNetworkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            foreach (var adapter in targetNetworkInterfaces)
            {
                if (adapter.OperationalStatus != System.Net.NetworkInformation.OperationalStatus.Up) continue;

                if (adapter.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Ethernet && adapter.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Wireless80211) continue;

                var interfaceProperties = adapter.GetIPProperties();

                var unicastAddresses = interfaceProperties.UnicastAddresses.Where(info => info.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();
                var gatewayAddresses = interfaceProperties.GatewayAddresses.ToArray();

                if (unicastAddresses.Length <= 0) continue;

                var unicastAddress = unicastAddresses[0];

                if (!unicastAddress.IsDnsEligible) continue;

                var network = System.Net.IPNetwork.Parse(unicastAddress.Address, unicastAddress.IPv4Mask);

                result = new Models.NetworkInformationModel
                {
                    AdapterId = adapter.Id,
                    UnicastIPAddress = unicastAddress.Address,
                    GatewayIPAddress = gatewayAddresses.Length > 0 ? gatewayAddresses[0].Address : null,
                    BroadcastIPAddress = network.Broadcast
                };

                break;
            }

            return result;
        }
    }
}
