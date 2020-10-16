namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Supporter.SignalR.Connect.Clients
{
    public interface IFetchOrderDownloadSignalRClient
    {
        System.Threading.Tasks.Task NotifyProgressAsync(int index, long transferProgress, long transferLength);
        System.Threading.Tasks.Task NotifyEndAsync(string orderFolderName, System.Exception exception = null);
    }
}
