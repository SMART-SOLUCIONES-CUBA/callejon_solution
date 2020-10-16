using Microsoft.AspNetCore.SignalR.Client;

[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Helpers.FetchOrderDownloadRequestHelper))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP.Helpers
{
    public class FetchOrderDownloadRequestHelper : NMMediaStore.Helpers.Facade.IFetchOrderDownloadRequestHelper, Supporter.SignalR.Connect.Clients.IFetchOrderDownloadSignalRClient
    {
        private HubConnection hubConnection;

        private Supporter.Api.Common.Models.FetchOrderDownloadModel fetchOrderDownload;
        private System.Action<string,System.Exception> endCallback;
        private System.Action<int,long,long> progressCallaback;

        public async System.Threading.Tasks.Task RequestAsync(long fetchOrderId, string targetStorageDeviceRootPath, System.Action<string,System.Exception> endCallback, System.Action<int,long,long> progressCallaback)
        {
            fetchOrderDownload = new Supporter.Api.Common.Models.FetchOrderDownloadModel
            {
                FetchOrderId = fetchOrderId,
                TargetStorageDevicePath = targetStorageDeviceRootPath
            };

            this.endCallback = endCallback;
            this.progressCallaback = progressCallaback;

            hubConnection = new HubConnectionBuilder()
                .WithUrl(string.Format("http://{0}/hubs/fetchorderdownload", "127.0.0.1:5666"))
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<int,long,long>(nameof(NotifyProgressAsync), NotifyProgressAsync);
            hubConnection.On<string,System.Exception>(nameof(NotifyEndAsync), NotifyEndAsync);

            hubConnection.Closed += Connection_ClosedAsync;

            await hubConnection.StartAsync();

            await hubConnection.InvokeAsync(nameof(Supporter.SignalR.Connect.Hubs.IFetchOrderDownloadSignalRHub.BeginFetchOrderDownload), fetchOrderDownload);
        }

        private async System.Threading.Tasks.Task Connection_ClosedAsync(System.Exception arg)
        {
            var randomizer = new System.Random();

            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(randomizer.Next(0, 5)));

            await hubConnection.StartAsync();
        }

        public async System.Threading.Tasks.Task NotifyEndAsync(string orderFolderName, System.Exception exception = null)
        {
            endCallback?.Invoke(orderFolderName, exception);

            hubConnection.Closed -= Connection_ClosedAsync;

            hubConnection.Remove(nameof(NotifyProgressAsync));
            hubConnection.Remove(nameof(NotifyEndAsync));

            await hubConnection.StopAsync();
            
            await hubConnection.DisposeAsync();

            hubConnection = null;
        }

        public System.Threading.Tasks.Task NotifyProgressAsync(int index, long transferProgress, long transferLength)
        {
            progressCallaback?.Invoke(index, transferProgress, transferLength);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
