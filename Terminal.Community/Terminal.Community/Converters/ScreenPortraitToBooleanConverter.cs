namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class ScreenPortraitToBooleanConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation == Xamarin.Essentials.DisplayOrientation.Portrait;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
