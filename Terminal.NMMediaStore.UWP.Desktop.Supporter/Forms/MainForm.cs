using System;
using Windows.ApplicationModel.AppService;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Desktop.Supporter.Forms
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private AppServiceConnection appServiceConnection;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            appServiceConnection = new AppServiceConnection
            {
                AppServiceName = "SupportInteropService",
                PackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName
            };

            appServiceConnection.RequestReceived += AppServiceConnection_RequestReceived;
            appServiceConnection.ServiceClosed += AppServiceConnection_ServiceClosed;

            System.Threading.Tasks.Task.Run(async () =>
            {
                var status = await appServiceConnection.OpenAsync();
            });
        }

        private void AppServiceConnection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                mainNotifyIcon.ContextMenuStrip = notificationIconContextMenuStrip;

                Environment.Exit(0);
            }));
        }

        private async void AppServiceConnection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs eventArgs)
        {
            var supportInteropServiceResponse = new Windows.Foundation.Collections.ValueSet();

            var command = eventArgs.Request.Message[Common.Providers.ConstantProvider.COMMAND_REQUEST_VALUESET_KEY].ToString();
            var args = eventArgs.Request.Message[Common.Providers.ConstantProvider.ARGUMENT_MODEL_VALUESET_KEY]?.ToString();

            switch (command)
            {
                case Common.Providers.ConstantProvider.COMMAND_EXIT_ENVIRONMENT_REQUEST:
                    Environment.Exit(0);

                    break;

                case Common.Providers.ConstantProvider.COMMAND_DRIVEINFO_REQUEST:
                    var driveInfo = new System.IO.DriveInfo(args);

                    var model = new Common.Models.DriveInfoModel
                    {
                        AvailableFreeSpace = driveInfo.AvailableFreeSpace,
                        DriveFormat = driveInfo.DriveFormat,
                        DriveType = driveInfo.DriveType,
                        IsReady = driveInfo.IsReady,
                        Name = driveInfo.Name,
                        TotalFreeSpace = driveInfo.TotalFreeSpace,
                        TotalSize = driveInfo.TotalSize,
                        VolumeLabel = driveInfo.VolumeLabel
                    };

                    supportInteropServiceResponse.Add(Common.Providers.ConstantProvider.ARGUMENT_MODEL_VALUESET_KEY, Newtonsoft.Json.JsonConvert.SerializeObject(model));

                    break;

                case Common.Providers.ConstantProvider.COMMAND_HANDLEFETCHORDER_REQUEST:
                    var arg = Newtonsoft.Json.JsonConvert.DeserializeObject<Common.Models.FetchOrderDownloadModel>(args);

                    var orderFolderName = DateTime.Now.ToString("dd-MM-yyyy HH-mm");

                    try
                    {
                        var mediaStoreTerminalConfiguration = await NMMediaStore.Common.Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.MediaStoreTerminalConfigurationModel>("mediastore/mediastoreterminalconfigurations");

                        var fetchOrder = await NMMediaStore.Common.Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.FetchOrderModel>(string.Format("mediastore/fetchorders/{0}", arg.FetchOrderId));

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

                            var lastNotifiedProgressAt = DateTime.Now;

                            foreach (var remoteFile in files)
                            {
                                long iterationDataCount = 0;

                                using (var inputStream = remoteFile.GetInputStream())
                                {
                                    var extension = System.IO.Path.GetExtension(remoteFile.GetName());

                                    var localDestinationDirectoryInfo = new System.IO.DirectoryInfo(string.Format("{1}{0}Mediateca Nuevo Mundo{0}{3}{0}{2}{0}", System.IO.Path.DirectorySeparatorChar, arg.TargetStorageDevicePath, item.Slug, orderFolderName));

                                    if (!localDestinationDirectoryInfo.Exists)
                                    {
                                        localDestinationDirectoryInfo.Create();
                                    }

                                    using (var localFileStream = System.IO.File.OpenWrite(string.Format("{0}{2}{3}", localDestinationDirectoryInfo, arg.TargetStorageDevicePath, item.Slug, extension)))
                                    {
                                        var inputStreamLength = inputStream.Length;

                                        var totalRead = 0L;

                                        while (totalRead < inputStreamLength)
                                        {
                                            var data = new byte[8192];

                                            var amountRead = inputStream.Read(data, 0, 8192);

                                            localFileStream.Write(data, 0, amountRead);

                                            totalRead += amountRead;
                                            iterationDataCount += amountRead;

                                            var now = DateTime.Now;

                                            var lastProgressNotificationElpsedTime = now - lastNotifiedProgressAt;

                                            if (lastProgressNotificationElpsedTime.TotalSeconds > 1)
                                            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                                                NotifyFetchOrderHandlingProgressAsync(counter, iterationDataCount, iterationDataLength).ContinueWith(_ => { });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                                                lastNotifiedProgressAt = now;
                                            }
                                        }

                                        localFileStream.Flush();
                                    }
                                }
                            }
                        }

                        await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(250));

                        await NotifyFetchOrderHandlingEndAsync(orderFolderName);
                    }
                    catch (Exception exception)
                    {
                        await NotifyFetchOrderHandlingEndAsync(orderFolderName, exception);
                    }

                    break;
            }            

            await eventArgs.Request.SendResponseAsync(supportInteropServiceResponse);
        }

        private async System.Threading.Tasks.Task NotifyFetchOrderHandlingProgressAsync(int index, long transferProgress, long transferLength)
        {
            var supportInteropServiceRequest = new Windows.Foundation.Collections.ValueSet();

            supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.COMMAND_REQUEST_VALUESET_KEY, Common.Providers.ConstantProvider.COMMAND_NOTIFYFETCHORDERHANDLINGPROGRESS_REQUEST);
            supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_INDEX_VALUESET_KEY, index);
            supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_TRANSFER_PROGRESSED_VALUESET_KEY, transferProgress);
            supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_TRANSFER_LENGHT_VALUESET_KEY, transferLength);

            _ = await appServiceConnection.SendMessageAsync(supportInteropServiceRequest);
        }

        private async System.Threading.Tasks.Task NotifyFetchOrderHandlingEndAsync(string orderFolderName, System.Exception exception = null)
        {
            var supportInteropServiceRequest = new Windows.Foundation.Collections.ValueSet();

            supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.COMMAND_REQUEST_VALUESET_KEY, Common.Providers.ConstantProvider.COMMAND_NOTIFYFETCHORDERHANDLINGEND_REQUEST);
            supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGEND_FOLDERNAME_VALUESET_KEY, orderFolderName);
            
            if (exception != null)
            {
                supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGEND_EXCEPTION_VALUESET_KEY, Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            }

            _ = await appServiceConnection.SendMessageAsync(supportInteropServiceRequest);
        }
    }
}
