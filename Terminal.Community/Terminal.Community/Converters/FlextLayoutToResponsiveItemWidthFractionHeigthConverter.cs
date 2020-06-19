namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class FlextLayoutToResponsiveItemWidthFractionHeigthConverter : Xamarin.Forms.IValueConverter
    {
        public FlextLayoutToResponsiveItemWidthConverter WidthCalculator { get; set; }

        public double Fraction { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = WidthCalculator.Convert(value, targetType, parameter, culture);

            if (width == null) return null;

            return (double)width / Fraction;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
