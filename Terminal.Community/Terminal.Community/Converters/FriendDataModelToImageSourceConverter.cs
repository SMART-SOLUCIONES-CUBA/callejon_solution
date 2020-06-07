namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class FriendDataModelToImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var model = value as Models.Data.FriendDataModel;

            if (model.ImageData == null)
            {
                if (!model.IsMale)
                {
                    return Providers.ImageSourceProvider.Instance.ProfileFemaleDefault();
                }

                return Providers.ImageSourceProvider.Instance.ProfileMaleDefault();
            }

            return Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(model.ImageData));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
