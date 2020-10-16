namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class CategoryModelToTreeNameConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            return GetTreeName(value as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        private string GetTreeName(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel model)
        {
            return model.Parent != null ? string.Format("{0} | {1}", GetTreeName(model.Parent), model.Name) : model.Name;
        }
    }
}
