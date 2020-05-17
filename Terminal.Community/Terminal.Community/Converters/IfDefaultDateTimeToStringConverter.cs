namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class IfDefaultDateTimeToStringConverter : Xamarin.Forms.IValueConverter
    {
        public string DefaultDateTimeString { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return System.DateTime.MinValue.Equals(value) ? DefaultDateTimeString : ((System.DateTime)value).ToLongDateString();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
