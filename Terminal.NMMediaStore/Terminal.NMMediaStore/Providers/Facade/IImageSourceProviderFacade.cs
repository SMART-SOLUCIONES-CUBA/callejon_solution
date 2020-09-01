namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface IImageSourceProviderFacade
    {
        Xamarin.Forms.ImageSource GetByModel(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel model);
    }
}
