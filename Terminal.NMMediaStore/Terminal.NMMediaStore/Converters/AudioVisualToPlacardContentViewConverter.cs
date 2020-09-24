namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Converters
{
    public class AudioVisualToPlacardContentViewConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var model = value as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel;

            Xamarin.Forms.View result;

            if (model.PlacardImage != null)
            {
                var imageModelToImageSourceConverter = new ImageModelToImageSourceConverter();

                result = new FFImageLoading.Forms.CachedImage
                {
                    Source = (Xamarin.Forms.ImageSource)imageModelToImageSourceConverter.Convert(model.PlacardImage, targetType, parameter, culture),
                    Aspect = Xamarin.Forms.Aspect.AspectFill
                };
            }
            else
            {
                result = new Views.Wrapper.HorizontalPosterWrapperView(model);
            }

            return result;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
