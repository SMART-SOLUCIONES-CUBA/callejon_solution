namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    internal class BytesToImageSourceBindingConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(value as byte[]));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
