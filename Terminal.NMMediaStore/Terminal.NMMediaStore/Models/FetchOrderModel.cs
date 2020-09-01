namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models
{
    public class FetchOrderModel
    {
        public FetchOrderModel()
        {
            AudioVisuals = new System.Collections.ObjectModel.ObservableCollection<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel>();
        }

        public System.Collections.ObjectModel.ObservableCollection<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel> AudioVisuals { get; set; }
    }
}
