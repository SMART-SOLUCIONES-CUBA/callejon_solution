using System.Linq;
using System.Net.Sockets;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Net
{
    public class DataShareBroadcastSocketServer
    {
        private static DataShareBroadcastSocketServer instance;

        public static DataShareBroadcastSocketServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataShareBroadcastSocketServer();
                }

                return instance;
            }
        }

        public async System.Threading.Tasks.Task RunAsync(System.Threading.CancellationToken cancellationToken)
        {
            var broadcastEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Broadcast, Providers.ConstantsProvider.SOCKET_DATASHARE_BROADCAST_SERVER_PORT);

            var broadcaster = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);

            broadcaster.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);

            try
            {
                byte[] databaseFileContent = null;

                using (var databaseFile = System.IO.File.OpenRead(Helpers.LocalFileStorageHelper.GetDatabaseFilePath()))
                {
                    databaseFileContent = new byte[databaseFile.Length];

                    await databaseFile.ReadAsync(databaseFileContent, 0, databaseFileContent.Length);
                }

                cancellationToken.ThrowIfCancellationRequested();

                var imageFilePaths = new System.Collections.Generic.List<string>();

                using (var dbContext = Providers.DdContextProvider.GetDbContext())
                {
                    var entities = dbContext.Images.ToArray();

                    foreach (var entity in entities)
                    {
                        imageFilePaths.Add(Helpers.LocalFileStorageHelper.GetImageFilePath(entity.Id));
                    }
                }

                cancellationToken.ThrowIfCancellationRequested();

                broadcaster.SendTo(System.BitConverter.GetBytes(databaseFileContent.Length), broadcastEndPoint);

                broadcaster.SendTo(databaseFileContent, broadcastEndPoint);

                cancellationToken.ThrowIfCancellationRequested();

                broadcaster.SendTo(System.BitConverter.GetBytes(imageFilePaths.Count), broadcastEndPoint);

                foreach (var imageFilePath in imageFilePaths)
                {
                    using (var imageFile = System.IO.File.OpenRead(imageFilePath))
                    {
                        var imageFileContent = new byte[imageFile.Length];

                        await imageFile.ReadAsync(imageFileContent, 0, databaseFileContent.Length);

                        broadcaster.SendTo(imageFileContent, broadcastEndPoint);
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            catch (System.Exception exception)
            {
            }

            broadcaster.Close();
        }
    }
}
