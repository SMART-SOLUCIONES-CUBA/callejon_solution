using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Common.Models;
using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Droid.Managers.FileStorageManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Droid.Managers
{
    public class FileStorageManager : NMMediaStore.Managers.Facade.IFileStorageManagerFacade
    {
        private Action<string, TimeSpan, Exception> fetchOrderHandlingEndCallbackAction;
        private Action<int, double, long, long, TimeSpan> fetchOrderHandlingProgressCallbackAction;
        private Action targeStorageRemovedCallbackAction;

        private FetchOrderDownloadModel currentFetchOrder;
        private StorageModel target;
        private System.Diagnostics.Stopwatch stopwatch;

        private void NotifyFetchOrderHandlingProgress(int audioVisualIndex, long transferLength, long transferedByteCount, double progress)
        {
            fetchOrderHandlingProgressCallbackAction?.Invoke(audioVisualIndex, progress, transferLength, transferedByteCount, stopwatch.Elapsed);
        }

        private void NotifyFetchOrderHandlingEnd(string orderFolderName, Exception exception = null)
        {
            currentFetchOrder = null;

            stopwatch.Stop();

            fetchOrderHandlingEndCallbackAction?.Invoke(orderFolderName, stopwatch.Elapsed, exception);

            stopwatch = null;
        }

        #region IFileStorageManagerFacade implementation

        public bool IsFetchOrderHandling { get => currentFetchOrder != null; }

        public void BindFetchOrderHandlingCallbacks(Action<string, TimeSpan, Exception> endCallback, Action<int, double, long, long, TimeSpan> progressCallaback)
        {
            fetchOrderHandlingEndCallbackAction = endCallback;
            fetchOrderHandlingProgressCallbackAction = progressCallaback;
        }

        public void UnbindFetchOderHandlingCallbacks()
        {
            fetchOrderHandlingEndCallbackAction = null;
            fetchOrderHandlingProgressCallbackAction = null;
        }

        public async System.Threading.Tasks.Task HandleFetchOrderAsync(long fetchOrderId, string targetStorageDeviceRootPath)
        {
            stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();

            currentFetchOrder = new FetchOrderDownloadModel
            {
                FetchOrderId = fetchOrderId,
                TargetStorageDevicePath = targetStorageDeviceRootPath
            };

            var orderFolderName = DateTime.Now.ToString("dd-MM-yyyy HH-mm");

            try
            {
                await Common.Helpers.FetchOrderHandlingHelper.DownloadFetchOrderAsync(currentFetchOrder.FetchOrderId, orderFolderName, currentFetchOrder.TargetStorageDevicePath, (int audioVisualDownloadingIndex, long transferLength, FluentFTP.FtpProgress progress) =>
                {
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        NotifyFetchOrderHandlingProgress(audioVisualDownloadingIndex, transferLength, progress.TransferredBytes, progress.Progress);
                    }).ContinueWith(_ => { });
                });

                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(250));

                NotifyFetchOrderHandlingEnd(orderFolderName);
            }
            catch (Exception exception)
            {
                NotifyFetchOrderHandlingEnd(orderFolderName, exception);
            }
        }

        public void BindStorageTargetCallbacks(Action targeRemovedCallbackAction)
        {
            targeStorageRemovedCallbackAction = targeRemovedCallbackAction;
        }

        public System.Threading.Tasks.Task<StorageModel[]> GetAvailableStoragesAsync()
        {
            var internalStorageTotalSpace = Android.OS.Environment.ExternalStorageDirectory.TotalSpace;
            
            var result = new System.Collections.Generic.List<StorageModel>
            {
                new StorageModel
                {
                    Id = Android.OS.Environment.ExternalStorageDirectory.Name,
                    Caption = Android.OS.Environment.ExternalStorageDirectory.Name,
                    Family = Enumerations.StorageFamiliy.MicroSDCard,
                    Path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies).AbsolutePath,
                    TotalSpace = internalStorageTotalSpace,
                    UsedSpace = internalStorageTotalSpace - Android.OS.Environment.ExternalStorageDirectory.FreeSpace
                }
            };

            return System.Threading.Tasks.Task.FromResult(result.ToArray());
        }

        public System.Threading.Tasks.Task ReloadTargetStorageAsync()
        {
            var internalStorageTotalSpace = Android.OS.Environment.ExternalStorageDirectory.TotalSpace;

            target = new StorageModel
            {
                Id = target.Id,
                Caption = target.Caption,
                Family = Enumerations.StorageFamiliy.MicroSDCard,
                Path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies).AbsolutePath,
                TotalSpace = internalStorageTotalSpace,
                UsedSpace = internalStorageTotalSpace - Android.OS.Environment.ExternalStorageDirectory.FreeSpace
            };

            return System.Threading.Tasks.Task.CompletedTask;
        }

        public StorageModel GetTargetStorage()
        {
            return target;
        }

        public void TargetStorage(StorageModel target)
        {
            this.target = target;
        }

        #endregion
    }
}