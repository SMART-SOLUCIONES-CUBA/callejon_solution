namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Page
{
    public class BillboardPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private System.Collections.ObjectModel.ObservableCollection<View.CinemaViewModel> cinemas;

        public System.Collections.ObjectModel.ObservableCollection<View.CinemaViewModel> Cinemas { get => cinemas; set => SetProperty(ref cinemas, value); }
    }
}
