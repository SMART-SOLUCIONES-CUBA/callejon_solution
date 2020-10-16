using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class VirtualStorageUsedBoxWidthConverter : Xamarin.Forms.IValueConverter
    {
        public Xamarin.Forms.ContentPage Page { get; set; }

        public double MinValue { get; set; }

        public VirtualStorageUsedBoxWidthConverter()
        {
            MinValue = 1;
        }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;

            if (width <= 0) return 0;

            var model = Page.BindingContext as Models.Page.Facade.IMasterPageModelWrapper;

            if (model == null || model.MasterBindingContext.Target.Storage == null) return 0.5d;

            var result = width * (model.MasterBindingContext.Target.Storage.UsedSpace + model.MasterBindingContext.FetchOrderAudioVisuals.ToStorageSpace()) / model.MasterBindingContext.Target.Storage.TotalSpace;

            return result < MinValue ? 1 : result;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}