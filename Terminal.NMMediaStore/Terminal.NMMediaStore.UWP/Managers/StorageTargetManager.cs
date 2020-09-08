using System;

[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Managers.StorageTargetManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Managers
{
    public class StorageTargetManager : NMMediaStore.Managers.Facade.IStorageTargetManagerFacade
    {
        private const string FREE_SPACE_PROPERTY_NAME = "System.FreeSpace";
        private const string TOTAL_SPACE_PROPERTY_NAME = "System.Capacity";

        private Models.StorageModel target;

        public async System.Threading.Tasks.Task<Models.StorageModel[]> GetAvailableStoragesAsync()
        {
            var externalDevices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Devices.Portable.StorageDevice.GetDeviceSelector(), null);

            var result = new System.Collections.Generic.List<Models.StorageModel>();

            using (var httpClient = new System.Net.Http.HttpClient())
            {
                foreach (var externalDevice in externalDevices)
                {
                    var storageDevice = Windows.Devices.Portable.StorageDevice.FromId(externalDevice.Id);

                    var response = await httpClient.GetAsync(string.Format("http://127.0.0.1:5000/api/driveinfo/{0}", storageDevice.Path));

                    var responseContent = await response.Content.ReadAsStringAsync();

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
            }

            return result.ToArray();
        }

        public Models.StorageModel GetStorageTarget()
        {
            return target;
        }

        public void TargetStorage(Models.StorageModel target)
        {
            this.target = target;
        }
    }
}
