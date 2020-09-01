using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class ImageSourceProvider : Facade.IImageSourceProviderFacade
    {
        public Xamarin.Forms.ImageSource GetByModel(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ImageModel model)
        {
            return Xamarin.Forms.ImageSource.FromUri(new System.Uri(string.Format("http://127.0.0.1/{0}{1}", model.Id, model.ContentType.ToImageExtension())));
            // TODO Restore expected behavior!!
            //return Xamarin.Forms.ImageSource.FromUri(new System.Uri(string.Format("http://sgc.elcallejon.net/media/images/{0}", model.Id)));
        }
    }
}
