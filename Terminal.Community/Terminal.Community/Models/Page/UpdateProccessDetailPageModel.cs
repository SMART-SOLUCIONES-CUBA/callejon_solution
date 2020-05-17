namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class UpdateProccessDetailPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string step;
        private string detail;
        private int progress;

        public string Step { get => step; set => SetProperty(ref step, value); }

        public string Detail { get => detail; set => SetProperty(ref detail, value); }

        public int Progress { get => progress; set => SetProperty(ref progress, value); }
    }
}
