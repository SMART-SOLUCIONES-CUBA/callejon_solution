using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using System.Collections.Generic;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class HomePageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private AdvertisementModel[] advertisements;
        private List<View.AudioVisualViewModel> popularAudioVisuals;
        private List<View.AudioVisualViewModel> lastAudioVisuals;
        private List<View.AudioVisualViewModel> recomendedAudioVisuals;

        public AdvertisementModel[] Advertisements { get => advertisements; set => SetProperty(ref advertisements, value); }

        public List<View.AudioVisualViewModel> PopularAudioVisuals { get => popularAudioVisuals; set => SetProperty(ref popularAudioVisuals, value); }

        public List<View.AudioVisualViewModel> LastAudioVisuals { get => lastAudioVisuals; set => SetProperty(ref lastAudioVisuals, value); }

        public List<View.AudioVisualViewModel> RecomendedAudioVisuals { get => recomendedAudioVisuals; set => SetProperty(ref recomendedAudioVisuals, value); }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            Advertisements = null;

            PopularAudioVisuals?.Clear();
            PopularAudioVisuals = null;

            LastAudioVisuals?.Clear();
            LastAudioVisuals = null;

            RecomendedAudioVisuals?.Clear();
            RecomendedAudioVisuals = null;
        }
    }
}
