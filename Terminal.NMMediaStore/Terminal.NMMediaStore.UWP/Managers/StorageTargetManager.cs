[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Managers.StorageTargetManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Managers
{
    public class StorageTargetManager : NMMediaStore.Managers.Facade.IStorageTargetManagerFacade
    {
        private Models.StorageModel target;

        public StorageTargetManager()
        {
            // TODO Remove Mock initialization!!

            target = new Models.StorageModel
            {
                Caption = "(E:) Memoria de Miguel 16GB",
                Family = Enumerations.StorageFamiliy.UsbFlashDrive,
                TotalSpace = 8160437248,
                UsedSpace = 566231040,
                Path = @"E:\"
            };
        }

        public Models.StorageModel[] GetAvailableStorages()
        {
            // TODO Implement expected behavior!!
            throw new System.NotImplementedException();
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
