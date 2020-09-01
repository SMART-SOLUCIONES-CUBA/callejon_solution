using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddAudioVisualForFetch(this Xamarin.Forms.Application target, SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel model)
        {
            ((target.MainPage as Xamarin.Forms.MasterDetailPage).Master.BindingContext as Models.Page.MasterPageModel).FetchOrderAudioVisuals.Insert(0, model);
        }

        public static void RemoveAudioVisualForFetch(this Xamarin.Forms.Application target, SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel model)
        {
            var bindingContextModel = (target.MainPage as Xamarin.Forms.MasterDetailPage).Master.BindingContext as Models.Page.MasterPageModel;

            var removeTarget = bindingContextModel.FetchOrderAudioVisuals.First(item => item.Id == model.Id);

            bindingContextModel.FetchOrderAudioVisuals.Remove(removeTarget);
        }

        public static bool HasAudioVisualForFetch(this Xamarin.Forms.Application target, SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel model)
        {
            return ((target.MainPage as Xamarin.Forms.MasterDetailPage).Master.BindingContext as Models.Page.MasterPageModel).FetchOrderAudioVisuals.Any(item => item.Id == model.Id);
        }
    }
}
