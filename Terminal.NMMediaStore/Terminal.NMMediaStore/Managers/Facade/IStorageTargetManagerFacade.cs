namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers.Facade
{
    public interface IStorageTargetManagerFacade
    {
        void ConfigurateCallbacks(System.Action targeRemovedCallbackAction);

        Models.StorageModel GetStorageTarget();

        void TargetStorage(Models.StorageModel target);

        System.Threading.Tasks.Task<Models.StorageModel[]> GetAvailableStoragesAsync();
    }
}
