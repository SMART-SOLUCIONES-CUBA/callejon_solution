namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class AudioVisualToLanguageDetailConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var model = value as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel;

            var result = new Xamarin.Forms.FormattedString();

            result.Spans.Add(new Xamarin.Forms.Span
            {
                Text = "(Audio: "
            });
            result.Spans.Add(new Xamarin.Forms.Span
            {
                Text = model.Language.Name
            });

            if (model.HasSubtitle)
            {
                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = " + Subtítulos en Español"
                });
            }

            result.Spans.Add(new Xamarin.Forms.Span
            {
                Text = ")"
            });

            return result;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
