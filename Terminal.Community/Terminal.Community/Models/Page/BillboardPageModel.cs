namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class BillboardPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private DLToolkit.Forms.Controls.FlowObservableCollection<View.CinemaViewModel> cinemas;

        public DLToolkit.Forms.Controls.FlowObservableCollection<View.CinemaViewModel> Cinemas { get => cinemas; set => SetProperty(ref cinemas, value); }
    }
}
