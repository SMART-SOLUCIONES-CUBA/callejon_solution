namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.View
{
    public abstract class AbstractChoiseViewModel : XFToolKit.Mvvm.Models.BaseBindableModel
    {
        private bool choised;

        public bool Choised { get => choised; set => SetProperty(ref choised, value); }
    }
}
