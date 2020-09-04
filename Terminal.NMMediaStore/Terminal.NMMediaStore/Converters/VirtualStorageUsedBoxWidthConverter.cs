using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class VirtualStorageUsedBoxWidthConverter : Xamarin.Forms.IValueConverter
    {
        public Xamarin.Forms.ContentPage Page { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;

            if (width <= 0) return 0;

            var model = Page.BindingContext as Models.Page.MasterPageModel;

            if (model == null) return 0;

            return width * (model.Target.Storage.UsedSpace + model.FetchOrderAudioVisuals.ToStorageSpace()) / model.Target.Storage.TotalSpace;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
