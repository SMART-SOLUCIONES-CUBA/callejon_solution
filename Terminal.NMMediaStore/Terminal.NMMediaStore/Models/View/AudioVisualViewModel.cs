namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.View
{
    public class AudioVisualViewModel : XFToolKit.Mvvm.Models.BaseBindableModel
    {
        private bool isMarkedForFetch;

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel AudioVisual { get; set; }

        public bool IsMarkedForFetch { get => isMarkedForFetch; set => SetProperty(ref isMarkedForFetch, value); }
    }
}
