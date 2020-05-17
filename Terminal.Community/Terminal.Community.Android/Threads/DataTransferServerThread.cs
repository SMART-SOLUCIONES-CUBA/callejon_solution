namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Threads
{
    public class DataTransferServerThread : Java.Lang.Thread
    {
        private Java.Net.Socket socket;

        public override void Run()
        {
            base.Run();

            var serverSocket = new Java.Net.ServerSocket(Providers.ConstantsProvider.DATA_TRANSFER_SOCKET_PORT);

            socket = serverSocket.Accept();
        }

        public override void Interrupt()
        {
            base.Interrupt();

            socket.Close();
        }
    }
}