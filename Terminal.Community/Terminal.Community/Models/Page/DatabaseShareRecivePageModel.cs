namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class DatabaseShareRecivePageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private bool isReceivingBundle;
        private double bundleReceivingProgress;
        private Data.FriendDataModel sender;

        public DatabaseShareRecivePageModel()
        {
            Candidates = new System.Collections.ObjectModel.ObservableCollection<View.DataShareSenderViewModel>();
        }

        public System.Collections.ObjectModel.ObservableCollection<View.DataShareSenderViewModel> Candidates { get; set; }

        public Data.FriendDataModel Sender { get => sender; set => SetProperty(ref sender, value); }

        public bool IsReceivingBundle { get => isReceivingBundle; set => SetProperty(ref isReceivingBundle, value); }

        public double BundleReceivingProgress { get => bundleReceivingProgress; set => SetProperty(ref bundleReceivingProgress, value); }
    }
}
