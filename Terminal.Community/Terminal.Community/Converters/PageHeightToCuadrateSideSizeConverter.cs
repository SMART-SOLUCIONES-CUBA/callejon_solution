namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class PageHeightToCuadrateSideSizeConverter : Xamarin.Forms.IValueConverter
    {
        public Xamarin.Forms.Page Page { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var pageWidth = (double) value;

            if (pageWidth == -1) return value;

            var fraction = parameter != null ? float.Parse(parameter.ToString()) : 1;

            return System.Math.Min(Page.Width, Page.Height) * fraction;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
