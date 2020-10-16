namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Supporter.SignalR.Connect.Hubs
{
    public interface IFetchOrderDownloadSignalRHub
    {
        void BeginFetchOrderDownload(Api.Common.Models.FetchOrderDownloadModel downloadFetchOrder);
    }
}
