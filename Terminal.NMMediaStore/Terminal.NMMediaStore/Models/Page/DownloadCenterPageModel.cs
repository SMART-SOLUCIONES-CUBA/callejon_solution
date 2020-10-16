namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class DownloadCenterPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel, Facade.IMasterPageModelWrapper
    {
        private int downloadingIndex;
        private decimal progressPercent;
        private long transferProgress;
        private long transferLength;
        private System.TimeSpan time;

        public MasterPageModel MasterBindingContext { get; set; }

        public int DownloadingIndex { get => downloadingIndex; set => SetProperty(ref downloadingIndex, value); }

        public decimal ProgressPercent { get => progressPercent; set => SetProperty(ref progressPercent, value); }

        public long TransferProgress { get => transferProgress; set => SetProperty(ref transferProgress, value); }

        public long TransferLength { get => transferLength; set => SetProperty(ref transferLength, value); }

        public System.TimeSpan Time { get => time; set => SetProperty(ref time, value); }
    }
}
