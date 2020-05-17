namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class MasterPageFractionConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object rawValue, System.Type targetType, object rawParameter, System.Globalization.CultureInfo culture)
        {
            var value = System.Convert.ToDouble(rawValue);

            if (value <= 0)
            {
                value = (Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage)?.MasterBounds.Width ?? Helpers.SizeProvisioningHelper.Instance.MasterPageDefaultWidth;
            }

            var paramenter = System.Convert.ToDouble(rawParameter, System.Globalization.CultureInfo.InvariantCulture);

            return value / paramenter;
        }

        public object ConvertBack(object rawValue, System.Type targetType, object rawParameter, System.Globalization.CultureInfo culture)
        {
            var value = System.Convert.ToDouble(rawValue);
            var paramenter = System.Convert.ToDouble(rawParameter, System.Globalization.CultureInfo.InvariantCulture);

            return value * paramenter;
        }
    }
}
