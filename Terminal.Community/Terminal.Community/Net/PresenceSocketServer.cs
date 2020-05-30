using System.Net;
using System.Net.Sockets;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Net
{
    public class PresenceSocketServer
    {
        private static PresenceSocketServer instance;

        public static PresenceSocketServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PresenceSocketServer();
                }

                return instance;
            }
        }

        public void Run(Models.Data.FriendDataModel hostFriendDataModel, System.Threading.CancellationToken cancellationToken, System.Action<Models.Data.PresenceDataModel> presenceDataReceiveCallbackAction)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, Providers.ConstantsProvider.SOCKET_PRESENCE_UNICAST_SERVER_PORT);

            var listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);

            listener.Listen(10);

            var handler = listener.Accept();

            while (cancellationToken.IsCancellationRequested)
            {
                string requestMessage = null;

                var requestContent = new byte[1024];

                int requestContentLength = handler.Receive(requestContent);

                requestMessage += System.Text.Encoding.UTF8.GetString(requestContent, 0, requestContentLength);

                int endOfMessageMarkIndex = requestMessage.IndexOf(Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK);

                if (endOfMessageMarkIndex > -1)
                {
                    presenceDataReceiveCallbackAction.Invoke(Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Data.PresenceDataModel>(requestMessage));

                    var responseMessageContent = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}{1}", Newtonsoft.Json.JsonConvert.SerializeObject(hostFriendDataModel), Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK));

                    handler.Send(responseMessageContent);

                    //handler.Shutdown(SocketShutdown.Send);
                }
            }

            handler.Shutdown(SocketShutdown.Both);

            handler.Close();
        }
    }
}