using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class SearchModelToInlineSummaryFormattedTextConverter : Xamarin.Forms.IValueConverter
    {
        public Xamarin.Forms.Color CaptionColor { get; set; }

        public Xamarin.Forms.Color ValueColor { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var model = value as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel;

            var result = new Xamarin.Forms.FormattedString();

            if (!string.IsNullOrEmpty(model.TitleTemathic))
            {
                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = "Título Temática: ",
                    TextColor = CaptionColor
                });
                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = model.TitleTemathic,
                    TextColor = ValueColor
                });
            }

            if (model.Category != null)
            {
                result.AddSepparatorIfRequired(CaptionColor);

                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = "Categoría: ",
                    TextColor = CaptionColor
                });
                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = model.Category.Name,
                    TextColor = ValueColor
                });
            }

            if (model.Country != null)
            {
                result.AddSepparatorIfRequired(CaptionColor);

                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = "País: ",
                    TextColor = CaptionColor
                });
                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = model.Country.Name,
                    TextColor = ValueColor
                });
            }

            if (model.ProducerHouse != null)
            {
                result.AddSepparatorIfRequired(CaptionColor);

                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = "Casa Productora: ",
                    TextColor = CaptionColor
                });
                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = model.ProducerHouse.Name,
                    TextColor = ValueColor
                });
            }

            if (model.Year > 0)
            {
                result.AddSepparatorIfRequired(CaptionColor);

                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = "Año: ",
                    TextColor = CaptionColor
                });
                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = model.Year.ToString(),
                    TextColor = ValueColor
                });
            }

            if (model.Genres != null && model.Genres.Length > 0)
            {
                result.AddSepparatorIfRequired(CaptionColor);

                var names = model.Genres.Select(item => item.Name).ToArray();

                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = "Géneros: ",
                    TextColor = CaptionColor
                });
                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = string.Join(", ", names),
                    TextColor = ValueColor
                });
            }

            if (model.Personalities != null && model.Personalities.Length > 0)
            {
                result.AddSepparatorIfRequired(CaptionColor);

                var names = model.Personalities.Select(item => item.Name).ToArray();

                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = "Elenco: ",
                    TextColor = CaptionColor
                });
                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = string.Join(", ", names),
                    TextColor = ValueColor
                });
            }

            if (model.Tags != null && model.Tags.Length > 0)
            {
                result.AddSepparatorIfRequired(CaptionColor);

                var names = model.Tags.Select(item => item.Name).ToArray();

                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = "Etiquetas: ",
                    TextColor = CaptionColor
                });
                result.Spans.Add(new Xamarin.Forms.Span
                {
                    Text = string.Join(", ", names),
                    TextColor = ValueColor
                });
            }

            return result;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
