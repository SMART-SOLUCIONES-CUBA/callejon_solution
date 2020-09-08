namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers.Facade
{
    public interface IStorageTargetManagerFacade
    {
        Models.StorageModel GetStorageTarget();

        void TargetStorage(Models.StorageModel target);

        System.Threading.Tasks.Task<Models.StorageModel[]> GetAvailableStoragesAsync();
    }
}
