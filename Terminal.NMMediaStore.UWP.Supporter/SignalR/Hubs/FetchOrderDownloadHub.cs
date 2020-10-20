using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Common.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Supporter.SignalR.Hubs
{
    public class FetchOrderDownloadHub : Hub<Connect.Clients.IFetchOrderDownloadSignalRClient>, Connect.Hubs.IFetchOrderDownloadSignalRHub
    {
        private readonly ILogger<FetchOrderDownloadHub> logger;
        private readonly Microsoft.Extensions.Options.IOptions<Options.PerformanceOptions> performanceOptions;

        private Connect.Clients.IFetchOrderDownloadSignalRClient client;

        public FetchOrderDownloadHub(Microsoft.Extensions.Options.IOptions<Options.PerformanceOptions> performanceOptions, ILogger<FetchOrderDownloadHub> logger)
        {
            this.logger = logger;
            this.performanceOptions = performanceOptions;
        }

        public override System.Threading.Tasks.Task OnConnectedAsync()
        {
            client = Clients.Caller;

            return base.OnConnectedAsync();
        }

        public override System.Threading.Tasks.Task OnDisconnectedAsync(System.Exception exception)
        {
            client = null;

            return base.OnDisconnectedAsync(exception);
        }

        public void BeginFetchOrderDownload(Api.Common.Models.FetchOrderDownloadModel downloadFetchOrder)
        {
            client = Clients.Caller;

            System.Threading.Tasks.Task.Run(async () =>
            {
                var orderFolderName = System.DateTime.Now.ToString("dd-MM-yyyy h-mm tt"); 
                
                try
                {
                    var mediaStoreTerminalConfiguration = await Common.Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.MediaStoreTerminalConfigurationModel>("mediastore/mediastoreterminalconfigurations");

                    var fetchOrder = await Common.Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.FetchOrderModel>(string.Format("mediastore/fetchorders/{0}", downloadFetchOrder.FetchOrderId));

                    var auth = new SharpCifs.Smb.NtlmPasswordAuthentication("ElCallejon.net", mediaStoreTerminalConfiguration.SharingUser, mediaStoreTerminalConfiguration.SharingPassword);

                    var counter = 0;

                    foreach (var item in fetchOrder.AudioVisuals)
                    {
                        counter++;

                        var audioVisualFolder = new SharpCifs.Smb.SmbFile(string.Format("smb://elcallejon.net/{0}/{1}/{2}/{3}/", mediaStoreTerminalConfiguration.SharingRootFolder, mediaStoreTerminalConfiguration.SharingAudioVisualsFolder, item.Category.StorageRelativePath, item.Slug), auth);

                        var files = audioVisualFolder.ListFiles();

                        long iterationDataLength = 0;

                        foreach (var remoteFile in files)
                        {
                            iterationDataLength += remoteFile.Length();
                        }

                        foreach (var remoteFile in files)
                        {
                            long iterationDataCount = 0;

                            using (var inputStream = remoteFile.GetInputStream())
                            {
                                var extension = System.IO.Path.GetExtension(remoteFile.GetName());

                                var localDestinationDirectoryInfo = new System.IO.DirectoryInfo(string.Format("{1}{0}Mediateca Nuevo Mundo{0}{3}{0}{2}{0}", System.IO.Path.DirectorySeparatorChar, downloadFetchOrder.TargetStorageDevicePath, item.Slug, orderFolderName));

                                if (!localDestinationDirectoryInfo.Exists)
                                {
                                    localDestinationDirectoryInfo.Create();
                                }

                                using (var localFileStream = System.IO.File.OpenWrite(string.Format("{0}{2}{3}", localDestinationDirectoryInfo, downloadFetchOrder.TargetStorageDevicePath, item.Slug, extension)))
                                {
                                    var inputStreamLength = inputStream.Length;

                                    var totalRead = 0L;

                                    while (totalRead < inputStreamLength)
                                    {
                                        var data = new byte[performanceOptions.Value.DownloadBufferLength];

                                        var amountRead = inputStream.Read(data, 0, performanceOptions.Value.DownloadBufferLength);

                                        localFileStream.Write(data, 0, amountRead);

                                        totalRead += amountRead;
                                        iterationDataCount += amountRead;

                                        client?.NotifyProgressAsync(counter, iterationDataCount, iterationDataLength).ContinueWith(_ => { });
                                    }

                                    localFileStream.Flush();
                                }
                            }
                        }
                    }

                    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromMilliseconds(250));

                    await client?.NotifyEndAsync(orderFolderName);
                }
                catch (System.Exception exception)
                {
                    await client?.NotifyEndAsync(orderFolderName, exception);
                }
            });
        }
    }
}
