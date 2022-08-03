using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Common.Helpers
{
    public static class FetchOrderHandlingHelper
    {
        public static async System.Threading.Tasks.Task DownloadFetchOrderAsync(long fetchOrderId, string orderFolderName, string targetStorageDevicePath, System.Action<int, long, FluentFTP.FtpProgress> progressCallbackAction)
        {
            var mediaStoreTerminalConfiguration = await HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.MediaStoreTerminalConfigurationModel>("mediastore/mediastoreterminalconfigurations");

            var fetchOrder = await HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.FetchOrderModel>(string.Format("mediastore/fetchorders/{0}", fetchOrderId));

            using (var ftpClient = new FluentFTP.FtpClient("elcallejon.net", mediaStoreTerminalConfiguration.SharingUser, mediaStoreTerminalConfiguration.SharingPassword))
            {
                await ftpClient.ConnectAsync();

                var audioVisualDownloadingIndex = 0;

                foreach (var audioVisual in fetchOrder.AudioVisuals)
                {
                    var localDestinationDirectoryInfo = new System.IO.DirectoryInfo(string.Format("{1}{0}Mediateca Nuevo Mundo{0}{3}{0}{2}{0}", System.IO.Path.DirectorySeparatorChar, targetStorageDevicePath, audioVisual.Slug, orderFolderName));

                    if (!localDestinationDirectoryInfo.Exists)
                    {
                        localDestinationDirectoryInfo.Create();
                    }

                    var audioVisualFolderItems = await ftpClient.GetListingAsync(string.Format("{0}/{1}/{2}", mediaStoreTerminalConfiguration.SharingAudioVisualsFolder, audioVisual.Category.StorageRelativePath, audioVisual.Slug));

                    long transferLength = 0;
                    var audioVisualFiles = new System.Collections.Generic.List<string>();

                    foreach (var audioVisualFile in audioVisualFolderItems)
                    {
                        transferLength += audioVisualFile.Size;
                        audioVisualFiles.Add(audioVisualFile.FullName);
                    }

                    await ftpClient.DownloadFilesAsync(localDestinationDirectoryInfo.FullName, audioVisualFiles, FluentFTP.FtpLocalExists.Resume, FluentFTP.FtpVerify.Retry, FluentFTP.FtpError.Throw, default, new FetchOrderDownloadProgressHelper(audioVisualDownloadingIndex, transferLength, progressCallbackAction));

                    audioVisualDownloadingIndex++;
                }

                await ftpClient.DisconnectAsync();
            }
        }
    }
}
