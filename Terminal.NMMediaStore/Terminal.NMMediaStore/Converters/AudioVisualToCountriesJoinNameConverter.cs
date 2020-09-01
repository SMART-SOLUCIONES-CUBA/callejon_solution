using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class AudioVisualToCountriesJoinNameConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var model = value as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel;

            var countries = new System.Collections.Generic.List<string> { model.Country.Name };

            if (model.CoCountries != null)
            {
                countries.AddRange(model.CoCountries.Select(item => item.Name));
            }

            return string.Join(", ", countries);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
