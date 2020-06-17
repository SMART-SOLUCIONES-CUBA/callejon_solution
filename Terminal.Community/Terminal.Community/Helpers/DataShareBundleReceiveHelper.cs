using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Helpers
{
    public class DataShareBundleReceiveHelper
    {
        private static DataShareBundleReceiveHelper instance;

        private System.Net.Sockets.Socket listener;

        private System.Threading.CancellationTokenSource cancellationTokenSource;

        public static DataShareBundleReceiveHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataShareBundleReceiveHelper();
                }

                return instance;
            }
        }

        public void CancelBundleSendListening()
        {
            cancellationTokenSource?.Cancel();

            listener?.Close();

            listener = null;
        }

        public void ListenForBundleSend(Models.Page.DatabaseShareRecivePageModel pageContext, System.Action<byte[]> completitionCallbackAction)
        {
            cancellationTokenSource = new System.Threading.CancellationTokenSource();

            ListenForBundleSend(pageContext, completitionCallbackAction, cancellationTokenSource.Token);
        }

        private void ListenForBundleSend(Models.Page.DatabaseShareRecivePageModel pageContext, System.Action<byte[]> completitionCallbackAction, System.Threading.CancellationToken cancellationToken)
        {
            var networkInformation = Providers.NetworkInformationProvider.GetNetworkInformation();

            var listenEndPoint = new System.Net.IPEndPoint(networkInformation.UnicastIPAddress, Providers.ConstantsProvider.DATASHARE_SENDER_BUNDLE_BROADCAST_SOCKET_PORT);

            listener = new System.Net.Sockets.Socket(networkInformation.UnicastIPAddress.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

            listener.Bind(listenEndPoint);
            listener.Listen(1);

            var bundleContent = new System.Collections.Generic.List<byte>();

            var handler = listener.Accept();

            while (!cancellationToken.IsCancellationRequested)
            {
                var buffer = new byte[Providers.ConstantsProvider.UNICAST_SOCKET_BUFFER_LENGHT];

                int receiveLength = 0;

                receiveLength = handler.Receive(buffer);

                bundleContent.AddRange(buffer.Take(receiveLength));

                var bundleContentLength = bundleContent.LongCount();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    pageContext.IsReceivingBundle = true;
                    pageContext.BundleReceivingProgress = (double)bundleContentLength / pageContext.Sender.Friend.DataLength;
                    pageContext.BusyProgressMessage = string.Format("Reciviendo ({0}%) ...", System.Convert.ToInt32(pageContext.BundleReceivingProgress * 100));
                });

                if (receiveLength <= 0 || bundleContentLength >= pageContext.Sender.Friend.DataLength) break;
            }

            handler.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            handler.Close();

            listener.Close();

            if (cancellationToken.IsCancellationRequested) return;

            completitionCallbackAction.Invoke(bundleContent.ToArray());
        }
    }
}
