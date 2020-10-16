namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Helpers.Facade
{
    public interface IFetchOrderDownloadRequestHelper
    {
        System.Threading.Tasks.Task RequestAsync(long fetchOrderId, string targetStorageDeviceRootPath, System.Action<string,System.Exception> endCallback, System.Action<int,long,long> progressCallaback);
    }
}
