namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Common.Helpers
{
    internal class FetchOrderDownloadProgressHelper : System.IProgress<FluentFTP.FtpProgress>
    {
        private readonly int audioVisualDownloadingIndex;
        private readonly long transferLength;
        private readonly System.Action<int, long, FluentFTP.FtpProgress> progressCallbackAction;

        private System.DateTime lastNotifiedProgressAt;

        public FetchOrderDownloadProgressHelper(int audioVisualDownloadingIndex, long transferLength, System.Action<int, long, FluentFTP.FtpProgress> progressCallbackAction)
        {
            this.audioVisualDownloadingIndex = audioVisualDownloadingIndex;
            this.transferLength = transferLength;
            this.progressCallbackAction = progressCallbackAction;
        }

        public void Report(FluentFTP.FtpProgress value)
        {
            var lastProgressNotificationElpsedTime = System.DateTime.Now - lastNotifiedProgressAt;

            if (lastProgressNotificationElpsedTime.TotalSeconds < 1) return;

            progressCallbackAction?.Invoke(audioVisualDownloadingIndex, transferLength, value);

            lastNotifiedProgressAt = System.DateTime.Now;
        }
    }
}
