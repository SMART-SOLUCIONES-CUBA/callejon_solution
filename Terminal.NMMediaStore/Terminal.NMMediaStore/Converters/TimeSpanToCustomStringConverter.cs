namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class TimeSpanToCustomStringConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var duration = (System.TimeSpan)value;

            var result = string.Format("{0} h {1} m", duration.Hours, duration.Minutes);

            if (duration.Hours < 1)
            {
                result = string.Format("{0} m", duration.Minutes);
            }
            else if (duration.Hours > 1)
            {
                result = string.Format("{0} h {1} m", duration.Hours, duration.Minutes);
            }

            return result;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}