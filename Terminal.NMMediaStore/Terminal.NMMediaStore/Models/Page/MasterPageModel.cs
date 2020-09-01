namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class MasterPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private View.SubscriptorViewModel subscriptor;
        private View.StorageViewModel targetStorage;

        public MasterPageModel()
        {
            FetchOrderAudioVisuals = new System.Collections.ObjectModel.ObservableCollection<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel>();
        }

        public View.SubscriptorViewModel Subscriptor { get => subscriptor; set => SetProperty(ref subscriptor, value); }

        public View.StorageViewModel TargetStorage { get => targetStorage; set => SetProperty(ref targetStorage, value); }

        public System.Collections.ObjectModel.ObservableCollection<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel> FetchOrderAudioVisuals { get; set; }
    }
}
