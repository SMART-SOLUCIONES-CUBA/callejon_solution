namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.View
{
    public class SubscriptorViewModel : XFToolKit.Mvvm.Models.BaseBindableModel
    {
        private string name;
        private string lastName;
        private Xamarin.Forms.ImageSource image;

        public string Name { get => name; set => SetProperty(ref name, value); }

        public string LastName { get => lastName; set => SetProperty(ref lastName, value); }

        public Xamarin.Forms.ImageSource ImageSource { get => image; set => SetProperty(ref image, value); }
    }
}
