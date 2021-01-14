namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers.Facade
{
    public interface IFileStorageManagerFacade
    {
        void BindStorageTargetCallbacks(System.Action targeRemovedCallbackAction);

        Models.StorageModel GetTargetStorage();

        System.Threading.Tasks.Task ReloadTargetStorageAsync();

        void TargetStorage(Models.StorageModel target);

        System.Threading.Tasks.Task<Models.StorageModel[]> GetAvailableStoragesAsync();

        bool IsFetchOrderHandling { get; }

        void BindFetchOrderHandlingCallbacks(System.Action<string, System.TimeSpan, System.Exception> endCallback, System.Action<int, long, long, System.TimeSpan> progressCallaback);

        void UnbindFetchOderHandlingCallbacks();

        System.Threading.Tasks.Task HandleFetchOrderAsync(long fetchOrderId, string targetStorageDeviceRootPath);
    }
}
