namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class PersonalityViewModelToImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var profile = value as Models.View.PersonalityViewModel;

            try
            {
                return Providers.ImageSourceProvider.Instance.Cached(profile.ImageId);
            }
            catch
            {
                if (profile != null && !profile.IsMale)
                {
                    return Providers.ImageSourceProvider.Instance.ProfileFemaleDefault();
                }

                return Providers.ImageSourceProvider.Instance.ProfileMaleDefault();
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
