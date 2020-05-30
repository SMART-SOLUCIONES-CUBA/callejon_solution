using SkiaSharp;
using SkiaSharp.QrCode;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Converters
{
    public class WiFiHostpotDetailToQRImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var model = value as Models.View.WiFiHostpotDetailViewModel;

            var imageSize = int.Parse(parameter.ToString());

            var uri = string.Format("WIFI:T:WPA;S:{0};P:{1};H:false;", model.SSID, model.Password);

            byte[] qrImageData = null;

            using (var generator = new QRCodeGenerator())
            {
                // Generate QrCode
                var qr = generator.CreateQrCode(uri, ECCLevel.L);

                // Render to canvas
                var info = new SKImageInfo(imageSize, imageSize);

                using (var surface = SKSurface.Create(info))
                {
                    surface.Canvas.Render(qr, info.Width, info.Height);

                    using (var image = surface.Snapshot())
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        qrImageData = data.ToArray();
                    }
                }
            }

            return Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(qrImageData));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
