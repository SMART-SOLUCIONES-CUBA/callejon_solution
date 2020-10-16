namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class CurrentStorageUsedBoxWidthConverter : Xamarin.Forms.IValueConverter
    {
        public Xamarin.Forms.ContentPage Page { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;

            if (width <= 0) return 0;

            var model = Page.BindingContext as Models.Page.Facade.IMasterPageModelWrapper;

            if (model == null || model.MasterBindingContext.Target.Storage == null) return 0.5d;

            return width * model.MasterBindingContext.Target.Storage.UsedSpace / model.MasterBindingContext.Target.Storage.TotalSpace;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}