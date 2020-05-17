namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class ScreenLandscapeToBooleanConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation == Xamarin.Essentials.DisplayOrientation.Landscape;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
