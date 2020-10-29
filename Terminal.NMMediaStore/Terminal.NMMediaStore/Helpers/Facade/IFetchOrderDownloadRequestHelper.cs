namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Helpers.Facade
{
    public interface IFetchOrderDownloadRequestHelper
    {
        System.Threading.Tasks.Task RequestAsync(long fetchOrderId, string targetStorageDeviceRootPath);

        void BindCallbacks(System.Action<string, System.TimeSpan, System.Exception> endCallback, System.Action<int, long, long, System.TimeSpan> progressCallaback);

        void UnbindCallbacks();

        bool IsDownloading { get; }
    }
}
