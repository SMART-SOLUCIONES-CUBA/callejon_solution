namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class ImageIdToImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Providers.ImageSourceProvider.Instance.Cached((System.Guid)value);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
