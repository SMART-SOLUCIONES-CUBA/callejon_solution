namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class ImageSourceProvider : Facade.IImageSourceProviderFacade
    {
        public Xamarin.Forms.ImageSource GetByModel(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel model)
        {
            return Xamarin.Forms.ImageSource.FromUri(new System.Uri(string.Format("http://sgc.elcallejon.net/media/default/image/{0}", model.Id)));
        }

        public Xamarin.Forms.ImageSource GetForSubscriptor(Models.SubscriptorModel model)
        {
            if (model.ThumbnailContent == null)
            {
                var randomizer = new System.Random();

                return Xamarin.Forms.ImageSource.FromResource(string.Format("SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Resources.Images.{0}.jpg", randomizer.Next(1, 1)), typeof(App).Assembly);
            }

            return Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(model.ThumbnailContent));
        }
    }
}
