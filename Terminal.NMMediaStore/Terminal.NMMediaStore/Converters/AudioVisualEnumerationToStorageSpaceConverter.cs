using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class AudioVisualEnumerationToStorageSpaceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return 0L;

            var items = value as System.Collections.Generic.IEnumerable<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel>;

            var fileSizeLongToStringConverter = new XFToolKit.Presentation.Converters.FileSizeLongToStringConverter();

            return fileSizeLongToStringConverter.Convert(items.ToStorageSpace(), targetType, parameter, culture);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
