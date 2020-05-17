namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class DateTimeToInvarianCultureTimeStringConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((System.DateTime)value).ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
