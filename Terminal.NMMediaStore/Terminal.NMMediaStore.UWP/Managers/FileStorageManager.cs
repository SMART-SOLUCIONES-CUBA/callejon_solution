using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Common.Models;
using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models;
using System;
using Windows.ApplicationModel.AppService;

[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Managers.FileStorageManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Managers
{
    public class FileStorageManager : NMMediaStore.Managers.Facade.IFileStorageManagerFacade
    {
        private System.Diagnostics.Stopwatch stopwatch;
        private StorageModel target;
        private Windows.Devices.Enumeration.DeviceWatcher targetWatcher;
        private FetchOrderDownloadModel currentFetchOrder;

        private Action<string, TimeSpan, Exception> fetchOrderHandlingEndCallbackAction;
        private Action<int, double, long, long, TimeSpan> fetchOrderHandlingProgressCallbackAction;
        private Action targeStorageRemovedCallbackAction;

        public FileStorageManager()
        {
            App.AppService_Connected += _AppServiceConnected;
            App.AppService_Disconnected += _AppServiceDisconnected;

        }

        private void _AppServiceConnected(object sender, AppServiceTriggerDetails e)
        {
            App.Connection.RequestReceived += AppServiceConnection_RequestReceived;
        }

        private async void _AppServiceDisconnected(object sender, EventArgs e)
        {
            if (App.IsForeground)
            {
                await Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }
        }

        private async void AppServiceConnection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs eventArgs)
        {
            var supportInteropServiceResponse = new Windows.Foundation.Collections.ValueSet();

            var command = eventArgs.Request.Message[Common.Providers.ConstantProvider.COMMAND_REQUEST_VALUESET_KEY].ToString();

            switch (command)
            {
                case Common.Providers.ConstantProvider.COMMAND_NOTIFYFETCHORDERHANDLINGPROGRESS_REQUEST:
                    fetchOrderHandlingProgressCallbackAction?.Invoke(
                        (int)eventArgs.Request.Message[Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_INDEX_VALUESET_KEY], 
                        (double)eventArgs.Request.Message[Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_TRANSFER_PROGRESS_VALUESET_KEY],
                        (long)eventArgs.Request.Message[Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_TRANSFER_LENGHT_VALUESET_KEY],
                        (long)eventArgs.Request.Message[Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGPROGRESS_TRANSFER_TRANSFERED_VALUESET_KEY],
                        stopwatch.Elapsed);

                    break;

                case Common.Providers.ConstantProvider.COMMAND_NOTIFYFETCHORDERHANDLINGEND_REQUEST:
                    currentFetchOrder = null;

                    stopwatch.Stop();

                    fetchOrderHandlingEndCallbackAction?.Invoke(eventArgs.Request.Message[Common.Providers.ConstantProvider.ARGUMENT_FETCHORDERHANDLINGEND_FOLDERNAME_VALUESET_KEY].ToString(), stopwatch.Elapsed, null);
                    
                    stopwatch = null;

                    break;
            }

            await eventArgs.Request.SendResponseAsync(supportInteropServiceResponse);
        }

        #region IFileStorageManagerFacade implementation

        public bool IsFetchOrderHandling { get => currentFetchOrder != null; }

        public void BindFetchOrderHandlingCallbacks(Action<string, TimeSpan, Exception> endCallback, Action<int, double, long, long, TimeSpan> progressCallaback)
        {
            fetchOrderHandlingEndCallbackAction = endCallback;
            fetchOrderHandlingProgressCallbackAction = progressCallaback;
        }

        public void UnbindFetchOderHandlingCallbacks()
        {
            fetchOrderHandlingEndCallbackAction = null;
            fetchOrderHandlingProgressCallbackAction = null;
        }

        public async System.Threading.Tasks.Task HandleFetchOrderAsync(long fetchOrderId, string targetStorageDeviceRootPath)
        {
            stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();

            currentFetchOrder = new FetchOrderDownloadModel
            {
                FetchOrderId = fetchOrderId,
                TargetStorageDevicePath = targetStorageDeviceRootPath
            };

            var supportInteropServiceRequest = new Windows.Foundation.Collections.ValueSet
            {
                { Common.Providers.ConstantProvider.COMMAND_REQUEST_VALUESET_KEY, Common.Providers.ConstantProvider.COMMAND_HANDLEFETCHORDER_REQUEST },
                { Common.Providers.ConstantProvider.ARGUMENT_MODEL_VALUESET_KEY, Newtonsoft.Json.JsonConvert.SerializeObject(currentFetchOrder) }
            };

            _ = await App.Connection.SendMessageAsync(supportInteropServiceRequest);
        }

        public void BindStorageTargetCallbacks(Action targeRemovedCallbackAction)
        {
            targeStorageRemovedCallbackAction = targeRemovedCallbackAction;
        }

        public async System.Threading.Tasks.Task<StorageModel[]> GetAvailableStoragesAsync()
        {
            var externalDevices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Devices.Portable.StorageDevice.GetDeviceSelector());

            var result = new System.Collections.Generic.List<StorageModel>();

            foreach (var externalDevice in externalDevices)
            {
                try
                {
                    var storageDevice = Windows.Devices.Portable.StorageDevice.FromId(externalDevice.Id);

                    var supportInteropServiceRequest = new Windows.Foundation.Collections.ValueSet();

                    supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.COMMAND_REQUEST_VALUESET_KEY, Common.Providers.ConstantProvider.COMMAND_DRIVEINFO_REQUEST);
                    supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.ARGUMENT_MODEL_VALUESET_KEY, storageDevice.Path);

                    var supportInteropServiceResponse = await App.Connection.SendMessageAsync(supportInteropServiceRequest);

                    var responseContent = supportInteropServiceResponse.Message[Common.Providers.ConstantProvider.ARGUMENT_MODEL_VALUESET_KEY].ToString();

                    var storageDriveInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<DriveInfoModel>(responseContent);

                    var totalSpace = storageDriveInfo.TotalSize;
                    var usedSpace = totalSpace - storageDriveInfo.TotalFreeSpace;

                    result.Add(new StorageModel
                    {
                        Id = externalDevice.Id,
                        Caption = storageDevice.DisplayName,
                        Family = storageDriveInfo.DriveType == System.IO.DriveType.Fixed ? Enumerations.StorageFamiliy.UsbHDDrive : Enumerations.StorageFamiliy.UsbFlashDrive,
                        Path = storageDevice.Path,
                        TotalSpace = totalSpace,
                        UsedSpace = usedSpace
                    });
                }
                catch
                {
                    continue;
                }
            }

            return result.ToArray();
        }

        public async System.Threading.Tasks.Task ReloadTargetStorageAsync()
        {
            var storageDevice = Windows.Devices.Portable.StorageDevice.FromId(target.Id);

            var supportInteropServiceRequest = new Windows.Foundation.Collections.ValueSet();

            supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.COMMAND_REQUEST_VALUESET_KEY, Common.Providers.ConstantProvider.COMMAND_DRIVEINFO_REQUEST);
            supportInteropServiceRequest.Add(Common.Providers.ConstantProvider.ARGUMENT_MODEL_VALUESET_KEY, storageDevice.Path);

            var supportInteropServiceResponse = await App.Connection.SendMessageAsync(supportInteropServiceRequest);

            var responseContent = supportInteropServiceResponse.Message[Common.Providers.ConstantProvider.ARGUMENT_MODEL_VALUESET_KEY].ToString();

            var storageDriveInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<DriveInfoModel>(responseContent);

            var totalSpace = storageDriveInfo.TotalSize;
            var usedSpace = totalSpace - storageDriveInfo.TotalFreeSpace;

            target = new StorageModel
            {
                Id = target.Id,
                Caption = storageDevice.DisplayName,
                Family = storageDriveInfo.DriveType == System.IO.DriveType.Fixed ? Enumerations.StorageFamiliy.UsbHDDrive : Enumerations.StorageFamiliy.UsbFlashDrive,
                Path = storageDevice.Path,
                TotalSpace = totalSpace,
                UsedSpace = usedSpace
            };
        }

        public StorageModel GetTargetStorage()
        {
            return target;
        }

        public void TargetStorage(StorageModel target)
        {
            if (this.target != null)
            {
                targetWatcher.Stop();

                targetWatcher.Removed -= TargetWatcher_Removed;

                targetWatcher = null;
            }

            this.target = target;

            if (target != null)
            {
                targetWatcher = Windows.Devices.Enumeration.DeviceInformation.CreateWatcher(Windows.Devices.Portable.StorageDevice.GetDeviceSelector());

                targetWatcher.Removed += TargetWatcher_Removed;

                targetWatcher.Start();
            }
        }

        #endregion

        private void TargetWatcher_Removed(Windows.Devices.Enumeration.DeviceWatcher sender, Windows.Devices.Enumeration.DeviceInformationUpdate args)
        {
            if (args.Id != target.Id) return;

            TargetStorage(null);

            targeStorageRemovedCallbackAction?.Invoke();
        }
    }
}
