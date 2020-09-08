using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class MasterPageModelToVirtualStorageUsedSpaceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return 0L;

            var model = value as Models.Page.MasterPageModel;

            if (model == null || model.Target.Storage == null) return 0L;

            var fileSizeLongToStringConverter = new XFToolKit.Presentation.Converters.FileSizeLongToStringConverter();

            return fileSizeLongToStringConverter.Convert(model.Target.Storage.UsedSpace + model.FetchOrderAudioVisuals.ToStorageSpace(), targetType, parameter, culture);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
