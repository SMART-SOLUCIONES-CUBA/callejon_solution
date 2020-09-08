using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddAudioVisualForFetch(this Xamarin.Forms.Application target, SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel model)
        {
            var bindingContextModel = (target.MainPage as Xamarin.Forms.MasterDetailPage).Master.BindingContext as Models.Page.MasterPageModel;

            if (bindingContextModel.Target.Storage == null) throw new Exceptions.UnspecifiedStorageDeviceException();

            var afterSpace =  bindingContextModel.Target.Storage.UsedSpace + bindingContextModel.FetchOrderAudioVisuals.ToStorageSpace() + model.FileSize;

            if (afterSpace > bindingContextModel.Target.Storage.TotalSpace) throw new Exceptions.InsufficientStorageSpaceException();

            bindingContextModel.FetchOrderAudioVisuals.Insert(0, model);

            ((target.MainPage as Xamarin.Forms.MasterDetailPage).Master as Pages.MasterPage).InvalidateTargetStorageStats();
        }

        public static void RemoveAudioVisualForFetch(this Xamarin.Forms.Application target, SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel model)
        {
            var bindingContextModel = (target.MainPage as Xamarin.Forms.MasterDetailPage).Master.BindingContext as Models.Page.MasterPageModel;

            var removeTarget = bindingContextModel.FetchOrderAudioVisuals.First(item => item.Id == model.Id);

            bindingContextModel.FetchOrderAudioVisuals.Remove(removeTarget);

            ((target.MainPage as Xamarin.Forms.MasterDetailPage).Master as Pages.MasterPage).InvalidateTargetStorageStats();
        }

        public static bool HasAudioVisualForFetch(this Xamarin.Forms.Application target, SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel model)
        {
            return ((target.MainPage as Xamarin.Forms.MasterDetailPage).Master.BindingContext as Models.Page.MasterPageModel).FetchOrderAudioVisuals.Any(item => item.Id == model.Id);
        }
    }
}
