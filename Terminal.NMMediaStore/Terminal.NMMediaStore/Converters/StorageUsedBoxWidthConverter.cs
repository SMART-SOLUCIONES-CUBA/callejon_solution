namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class StorageUsedBoxWidthConverter : Xamarin.Forms.IValueConverter
    {
        public Xamarin.Forms.View TotalUsedPageReferenceView { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (TotalUsedPageReferenceView.Width <= 0) return null;

            return TotalUsedPageReferenceView.Width * (double)value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
