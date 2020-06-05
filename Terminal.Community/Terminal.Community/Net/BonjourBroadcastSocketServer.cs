using System.Net.Sockets;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Net
{
    public class BonjourBroadcastSocketServer
    {
        private static BonjourBroadcastSocketServer instance;

        public static BonjourBroadcastSocketServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BonjourBroadcastSocketServer();
                }

                return instance;
            }
        }

        public async System.Threading.Tasks.Task RunAsync(Models.Data.FriendDataModel hostFriendDataModel, System.Threading.CancellationToken cancellationToken)
        {
            var broadcastEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Broadcast, Providers.ConstantsProvider.DATASHARE_SENDER_PRESENCE_BROADCAST_SOCKET_PORT);

            var broadcaster = new Socket(System.Net.IPAddress.Broadcast.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            broadcaster.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var broadcastMessageContent = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}{1}", Newtonsoft.Json.JsonConvert.SerializeObject(hostFriendDataModel), Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK));

                    broadcaster.SendTo(broadcastMessageContent, broadcastEndPoint);

                    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(5));
                }
            }
            catch (System.Exception exception)
            {

            }

            broadcaster.Close();
        }
    }
}
