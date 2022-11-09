namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class ImageSourceProvider : Facade.IImageSourceProviderFacade
    {
        public Xamarin.Forms.ImageSource GetByModel(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel model)
        {
            return GetByImageId(model.Id);
        }

        public Xamarin.Forms.ImageSource GetByModel(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.PersonalityModel model)
        {
            return GetByImageId(model.Image?.Id);
        }

        private Xamarin.Forms.ImageSource GetByImageId(System.Guid? imageId)
        {
            return Xamarin.Forms.ImageSource.FromUri(new System.Uri(imageId != null ? string.Format("http://sgc.elcallejon.net/media/default/image/{0}", imageId) : "http://sgc.elcallejon.net/img/default-person-picture.jpg"));
        }

        public Xamarin.Forms.ImageSource GetByModel(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel model)
        {
            return GetByImageId(model.ImageId);
        }
    }
}
