namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class MasterPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel, Facade.IMasterPageModelWrapper
    {
        private View.SubscriptorViewModel subscriptor;
        private View.StorageViewModel target;
        private int downloadingIndex;
        private decimal progressPercent;
        private long transferProgress;
        private long transferLength;
        private System.TimeSpan time;

        public MasterPageModel()
        {
            FetchOrderAudioVisuals = new System.Collections.ObjectModel.ObservableCollection<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel>();
        }

        public View.SubscriptorViewModel Subscriptor { get => subscriptor; set => SetProperty(ref subscriptor, value); }

        public View.StorageViewModel Target { get => target; set => SetProperty(ref target, value); }

        public int DownloadingIndex { get => downloadingIndex; set => SetProperty(ref downloadingIndex, value); }

        public decimal ProgressPercent { get => progressPercent; set => SetProperty(ref progressPercent, value); }

        public long TransferProgress { get => transferProgress; set => SetProperty(ref transferProgress, value); }

        public long TransferLength { get => transferLength; set => SetProperty(ref transferLength, value); }

        public System.TimeSpan Time { get => time; set => SetProperty(ref time, value); }

        public System.Collections.ObjectModel.ObservableCollection<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel> FetchOrderAudioVisuals { get; set; }

        public MasterPageModel MasterBindingContext => this;

        public void RaiseFetchOrderAudioVisualsChanged()
        {
            RaisePropertyChanged("FetchOrderAudioVisuals");
        }
    }
}
