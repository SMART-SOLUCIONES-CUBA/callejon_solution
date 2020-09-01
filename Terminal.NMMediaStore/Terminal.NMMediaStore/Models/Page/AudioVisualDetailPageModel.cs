namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.Page
{
    public class AudioVisualDetailPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel audioVisual;
        private long checkCount;
        private long downloadCount;
        private long commentCount;
        private bool isMarkedForFetch;

        public bool IsMarkedForFetch { get => isMarkedForFetch; set => SetProperty(ref isMarkedForFetch, value); }

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel AudioVisual { get => audioVisual; set => SetProperty(ref audioVisual, value); }

        public long CheckCount { get => checkCount; set => SetProperty(ref checkCount, value); }

        public long DownloadCount { get => downloadCount; set => SetProperty(ref downloadCount, value); }

        public long CommentCount { get => commentCount; set => SetProperty(ref commentCount, value); }
    }
}
