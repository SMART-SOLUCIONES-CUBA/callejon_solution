namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models.View
{
    public class SubscriptorViewModel : XFToolKit.Mvvm.Models.BaseBindableModel
    {
        private string name;
        private string lastName;
        private SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel image;

        public string Name { get => name; set => SetProperty(ref name, value); }

        public string LastName { get => lastName; set => SetProperty(ref lastName, value); }

        public SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel Image { get => image; set => SetProperty(ref image, value); }
    }
}
