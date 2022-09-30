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

                    var model = new NMMediaStore.Common.Models.DriveInfoModel
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
                    var arg = Newtonsoft.Json.JsonConvert.DeserializeObject<NMMediaStore.Common.Models.FetchOrderDownloadModel>(args);

                    var orderFolderName = DateTime.Now.ToString("dd-MM-yyyy HH-mm");

                    try
                    {
                        await NMMediaStore.Common.Helpers.FetchOrderHandlingHelper.DownloadFetchOrderAsync(arg.FetchOrderId, orderFolderName, arg.TargetStorageDevicePath, (int audioVisualDownloadingIndex, long transferLength, FluentFTP.FtpProgress progress) =>
                        {
                            NotifyFetchOrderHandlingProgressAsync(audioVisualDownloadingIndex, transferLength, progress.TransferredBytes, progress.Progress).ContinueWith(_ => { });
                        });

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

        private async System.Threading.Tasks.Task NotifyFetchOrderHandlingProgressAsync(int audioVisualIndex, long transferLength, long transferedByteCount, double progress)
        {
            var supportInteropServiceRequest = new Windows.Foundation.Collections.ValueSet
            {
                { Common.Providers.ConstantProvider.COMMAND_REQUEST_VALUESET_KEY, Common.Providers.ConstantProvider.COMMAND_NOTIFYFETCHORDERHANDLINGPROGRESS_REQUEST },
                { Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_INDEX_VALUESET_KEY, audioVisualIndex },
                { Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_TRANSFER_PROGRESS_VALUESET_KEY, progress },
                { Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_TRANSFER_LENGHT_VALUESET_KEY, transferLength },
                { Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_TRANSFER_TRANSFERED_VALUESET_KEY, transferedByteCount }
            };

            _ = await appServiceConnection.SendMessageAsync(supportInteropServiceRequest);
        }

        private async System.Threading.Tasks.Task NotifyFetchOrderHandlingEndAsync(string orderFolderName, Exception exception = null)
        {
            var supportInteropServiceRequest = new Windows.Foundation.Collections.ValueSet
            {
                { Common.Providers.ConstantProvider.COMMAND_REQUEST_VALUESET_KEY, Common.Providers.ConstantProvider.COMMAND_NOTIFYFETCHORDERHANDLINGEND_REQUEST },
                { Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGEND_FOLDERNAME_VALUESET_KEY, orderFolderName }
            };

            if (exception != null)
            {
                supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGEND_EXCEPTION_VALUESET_KEY, Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            }

            _ = await appServiceConnection.SendMessageAsync(supportInteropServiceRequest);
        }
    }
}
