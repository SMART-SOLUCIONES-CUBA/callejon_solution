namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class PageWidthToResponsiveColumnWidthConverter : Xamarin.Forms.IValueConverter
    {
        public int Columns { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;

            if (width ==-1) return null;            

            int margin = parameter == null ? 0 : int.Parse(parameter.ToString());

            return Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation == Xamarin.Essentials.DisplayOrientation.Portrait ? width : (width - margin) / Columns;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}