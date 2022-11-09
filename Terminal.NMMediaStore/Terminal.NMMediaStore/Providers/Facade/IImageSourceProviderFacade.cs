namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface IImageSourceProviderFacade
    {
        Xamarin.Forms.ImageSource GetByModel(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel model);

        Xamarin.Forms.ImageSource GetByModel(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.PersonalityModel model);

        Xamarin.Forms.ImageSource GetByModel(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel model);
    }
}
