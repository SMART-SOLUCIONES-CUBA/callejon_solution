namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class PersonalityModelToImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            return XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IImageSourceProviderFacade>().GetByModel(value as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.PersonalityModel);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
