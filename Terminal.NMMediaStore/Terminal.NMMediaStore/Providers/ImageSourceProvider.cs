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
                // TODO Get random default user image!!
                throw new System.InvalidOperationException();
            }

            return Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(model.ThumbnailContent));
        }
    }
}
