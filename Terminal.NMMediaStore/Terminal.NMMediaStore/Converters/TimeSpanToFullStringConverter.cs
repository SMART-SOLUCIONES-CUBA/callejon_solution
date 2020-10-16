namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class TimeSpanToFullStringConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var duration = (System.TimeSpan)value;

            var elpasedTime = string.Format("1 hora {0} minutos y {1} segundos", duration.Minutes, duration.Seconds);

            if (duration.Hours < 1)
            {
                elpasedTime = string.Format("{0} minutos y {1} segundos", duration.Minutes, duration.Seconds);

                if (duration.Minutes < 1)
                {
                    elpasedTime = string.Format("{0} segundos", duration.Seconds);
                }
                else if (duration.Minutes == 1)
                {
                    elpasedTime = string.Format("1 minutos y {0} segundos", duration.Seconds);
                }
            }
            else if (duration.Hours > 1)
            {
                elpasedTime = string.Format("{0} horas {1} minutos y {2} segundos", duration.Hours, duration.Minutes, duration.Seconds);
            }

            return elpasedTime;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
