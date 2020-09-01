namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class InvalidYearToReplacementStringConverter : Xamarin.Forms.IValueConverter
    {
        public string InvalidStringReplacement { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var year = (int)value;

            return year > 0 ? year.ToString() : InvalidStringReplacement;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
