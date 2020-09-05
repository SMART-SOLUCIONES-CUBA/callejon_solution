namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class StorageUsedBoxWidthToCornerRadiusConverter : Xamarin.Forms.IValueConverter
    {
        public double MaxWidth { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return default(Xamarin.Forms.CornerRadius);

            var width = (double)value;

            if (width <= 0) return default(Xamarin.Forms.CornerRadius);

            return new Xamarin.Forms.CornerRadius(width > MaxWidth ? MaxWidth / 2 : width / 2);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
