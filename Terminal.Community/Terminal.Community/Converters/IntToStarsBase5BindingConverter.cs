namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    internal class IntToStarsBase5BindingConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valoration = (int)value;

            var filledStarCharacter = '\ue083';
            var emptyStarCharacter = '\ue085';

            var result = string.Empty;

            for (int i = 1; i <= 5; i++)
            {
                if (i <= valoration)
                {
                    result += filledStarCharacter;
                }
                else
                {
                    result += emptyStarCharacter;
                }
            }

            return result;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
