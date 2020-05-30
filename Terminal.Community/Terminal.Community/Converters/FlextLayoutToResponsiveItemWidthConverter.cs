namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class FlextLayoutToResponsiveItemWidthConverter : Xamarin.Forms.IValueConverter
    {
        public double ItemMinWidth { get; set; }

        public int ItemHorizontalMargin { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var containerWidth = (double)value;

            if (containerWidth == -1) return null;

            var reazon = containerWidth / ItemMinWidth;

            int columnCount = System.Convert.ToInt32(System.Math.Round(reazon, System.MidpointRounding.ToEven));

            if (columnCount > reazon)
            {
                columnCount--;
            }

            return containerWidth / columnCount - ItemHorizontalMargin;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
