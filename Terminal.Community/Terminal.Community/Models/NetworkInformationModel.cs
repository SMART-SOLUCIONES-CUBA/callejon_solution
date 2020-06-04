namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models
{
    public class NetworkInformationModel
    {
        public string AdapterId { get; set; }

        public System.Net.IPAddress UnicastIPAddress { get; set; }

        public System.Net.IPAddress BroadcastIPAddress { get; set; }

        public System.Net.IPAddress GatewayIPAddress { get; set; }
    }
}
