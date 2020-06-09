namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class DateTimeToInvarianCultureRelativeDateTimeStringConverter : Xamarin.Forms.IValueConverter
    {
        public string DefaultValuePlaceholder { get; set; }

        public DateTimeToInvarianCultureRelativeDateTimeStringConverter()
        {
            DefaultValuePlaceholder = "-";
        }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var datetime = (System.DateTime)value;

            if (default == datetime) return DefaultValuePlaceholder;

            return string.Format("{0}, {1}", datetime.ToString("dddd, MMMM dd"), datetime.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
