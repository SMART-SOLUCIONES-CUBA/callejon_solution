namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class TimeSpanToCustomStringBindingConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var duration = (System.TimeSpan) value;

            var result = string.Format("{0} hora {1} minutos", duration.Hours, duration.Minutes);

            if (duration.Hours < 1)
            {
                result = string.Format("{0} minutos", duration.Minutes);
            }
            else if (duration.Hours > 1)
            {
                result = string.Format("{0} horas {1} minutos", duration.Hours, duration.Minutes);
            }

            return result;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
