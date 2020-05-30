namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class PagePortraitOrientationToBooleanConverter : Xamarin.Forms.IValueConverter
    {
        public PagePortraitOrientationToBooleanConverter()
        {
            Inverse = false;
        }

        public bool Inverse { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isLanscape = Xamarin.Forms.Device.Idiom != Xamarin.Forms.TargetIdiom.Phone || Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation == Xamarin.Essentials.DisplayOrientation.Landscape;

            return !Inverse ? !isLanscape : isLanscape;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
