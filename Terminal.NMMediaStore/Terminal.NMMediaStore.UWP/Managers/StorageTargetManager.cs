using System;

[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Managers.StorageTargetManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Managers
{
    public class StorageTargetManager : NMMediaStore.Managers.Facade.IStorageTargetManagerFacade
    {
        private Models.StorageModel target;
        private Windows.Devices.Enumeration.DeviceWatcher targetWatcher;

        private Action targeRemovedCallbackAction;

        public void ConfigurateCallbacks(Action targeRemovedCallbackAction)
        {
            this.targeRemovedCallbackAction = targeRemovedCallbackAction;
        }

        public async System.Threading.Tasks.Task<Models.StorageModel[]> GetAvailableStoragesAsync()
        {
            var externalDevices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Devices.Portable.StorageDevice.GetDeviceSelector());

            var result = new System.Collections.Generic.List<Models.StorageModel>();

            using (var httpClient = new System.Net.Http.HttpClient())
            {
                foreach (var externalDevice in externalDevices)
                {
                    var storageDevice = Windows.Devices.Portable.StorageDevice.FromId(externalDevice.Id);

                    var response = await httpClient.GetAsync(string.Format("http://127.0.0.1:{0}/api/driveinfo/{1}", Supporter.Api.Common.Constants.SUPPORT_KESTREL_PORT, storageDevice.Path));

                    var responseContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var storageDriveInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Supporter.Api.Common.Models.DriveInfoModel>(responseContent);

                        var totalSpace = storageDriveInfo.TotalSize;
                        var usedSpace = totalSpace - storageDriveInfo.TotalFreeSpace;

                        result.Add(new Models.StorageModel
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
            }

            return result.ToArray();
        }

        public async System.Threading.Tasks.Task<Models.StorageModel> GetReloadedTargetStorageAsync()
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var storageDevice = Windows.Devices.Portable.StorageDevice.FromId(target.Id);

                var response = await httpClient.GetAsync(string.Format("http://127.0.0.1:{0}/api/driveinfo/{1}", Supporter.Api.Common.Constants.SUPPORT_KESTREL_PORT, storageDevice.Path));

                var responseContent = await response.Content.ReadAsStringAsync();

                try
                {
                    var storageDriveInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Supporter.Api.Common.Models.DriveInfoModel>(responseContent);

                    var totalSpace = storageDriveInfo.TotalSize;
                    var usedSpace = totalSpace - storageDriveInfo.TotalFreeSpace;

                    target.Caption = storageDevice.DisplayName;
                    target.TotalSpace = totalSpace;
                    target.UsedSpace = usedSpace;
                }
                catch
                {
                    return target;
                }
            }

            return target;
        }

        public Models.StorageModel GetTargetStorage()
        {
            return target;
        }

        public void TargetStorage(Models.StorageModel target)
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

        private void TargetWatcher_Removed(Windows.Devices.Enumeration.DeviceWatcher sender, Windows.Devices.Enumeration.DeviceInformationUpdate args)
        {
            if (args.Id != target.Id) return;

            TargetStorage(null);

            targeRemovedCallbackAction?.Invoke();
        }
    }
}
