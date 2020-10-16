namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class MasterPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel, Facade.IMasterPageModelWrapper
    {
        private View.SubscriptorViewModel subscriptor;
        private View.StorageViewModel target;

        public MasterPageModel()
        {
            FetchOrderAudioVisuals = new System.Collections.ObjectModel.ObservableCollection<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel>();
        }

        public View.SubscriptorViewModel Subscriptor { get => subscriptor; set => SetProperty(ref subscriptor, value); }

        public View.StorageViewModel Target { get => target; set => SetProperty(ref target, value); }

        public System.Collections.ObjectModel.ObservableCollection<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel> FetchOrderAudioVisuals { get; set; }

        public MasterPageModel MasterBindingContext => this;

        public void RaiseFetchOrderAudioVisualsChanged()
        {
            RaisePropertyChanged("FetchOrderAudioVisuals");
        }
    }
}
