using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class CollectionToJoinedStringBindingConverter : Xamarin.Forms.IValueConverter
    {
        public string EmptyStringReplacement { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return EmptyStringReplacement;

            var collection = value as System.Collections.ICollection;

            if (collection.Count <= 0) return EmptyStringReplacement;

            var strings = new System.Collections.Generic.List<string>();

            foreach (var item in collection)
            {
                strings.Add(item.ToString());
            }

            return string.Join(", ", strings);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
