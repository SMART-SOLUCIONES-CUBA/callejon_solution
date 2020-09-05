namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class StorageUsedBoxWidthToHeightConverter : Xamarin.Forms.IValueConverter
    {
        public double MaxWidth { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return MaxWidth;

            var width = (double)value;

            return width > MaxWidth ? MaxWidth : width;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
