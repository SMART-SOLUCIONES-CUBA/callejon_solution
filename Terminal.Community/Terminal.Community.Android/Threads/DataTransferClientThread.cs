namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Threads
{
    public class DataTransferClientThread : Java.Lang.Thread
    {
        private readonly Java.Net.InetAddress hostAddress;

        public DataTransferClientThread(Java.Net.InetAddress hostAddress)
        {
            this.hostAddress = hostAddress;
        }

        public Java.Net.Socket Socket { get; set; }

        public override void Run()
        {
            base.Run();

            Socket = new Java.Net.Socket();

            Socket.Connect(new Java.Net.InetSocketAddress(hostAddress, Providers.ConstantsProvider.DATA_TRANSFER_SOCKET_PORT), 1000);
        }
    }
}