namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class PageHeightToCuadrateSideSizeConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var pageWidth = (double) value;

            if (pageWidth == -1) return value;

            var page = parameter as Xamarin.Forms.Page;

            return System.Math.Min(page.Width, page.Height);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
