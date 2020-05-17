using SkiaSharp.Views.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Providers
{
    internal sealed class ImageSourceProvider
    {
        private static ImageSourceProvider instance;

        public static ImageSourceProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImageSourceProvider();
                }

                return instance;
            }
        }

        public Xamarin.Forms.ImageSource FilmIconImageSource
        {
            get
            {
                using (var imageSKData = GetIconImageSKData("\uE053", Helpers.SizeProvisioningHelper.Instance.EventPersonalityOverviewImageSize, Xamarin.Forms.Color.Accent, Xamarin.Forms.Color.Transparent))
                {
                    return Xamarin.Forms.ImageSource.FromStream(imageSKData.AsStream);
                }
            }
        }

        public Xamarin.Forms.ImageSource UpdateIconImageSource
        {
            get
            {
                using (var imageSKData = GetIconImageSKData("\uE086", Helpers.SizeProvisioningHelper.Instance.EventPersonalityOverviewImageSize, Xamarin.Forms.Color.Accent, Xamarin.Forms.Color.Transparent))
                {
                    return Xamarin.Forms.ImageSource.FromStream(imageSKData.AsStream);
                }
            }
        }

        public Xamarin.Forms.ImageSource Cached(System.Guid id)
        {
            var filePath = Helpers.LocalFileStorageHelper.GetImageFilePath(id);

            if (!System.IO.File.Exists(filePath))
            {
                throw new System.IO.FileNotFoundException();
            }

            var optimizedImageFilePath = Helpers.LocalFileStorageHelper.GetResponsiveImageFilePath(id, System.Convert.ToInt32(System.Math.Min(Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width, Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height)));

            if (System.IO.File.Exists(optimizedImageFilePath))
            {
                return Xamarin.Forms.ImageSource.FromFile(optimizedImageFilePath);
            }

            var rawImageFilePath = Helpers.LocalFileStorageHelper.GetImageFilePath(id);

            if (!System.IO.File.Exists(rawImageFilePath))
            {
                throw new System.IO.FileNotFoundException();
            }

            return Xamarin.Forms.ImageSource.FromFile(rawImageFilePath);
        }

        public Xamarin.Forms.ImageSource ProfileMaleDefault()
        {
            return ProfileDefault("profile-male-default.jpg", "\uE08f");
        }

        public Xamarin.Forms.ImageSource ProfileFemaleDefault()
        {
            return ProfileDefault("profile-female-default.jpg", "\uE090");
        }

        private Xamarin.Forms.ImageSource ProfileDefault(string fileName, string unicode)
        {
            var imageFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), fileName);

            if (System.IO.File.Exists(imageFilePath))
            {
                return Xamarin.Forms.ImageSource.FromFile(imageFilePath);
            }

            using (var imageSKData = GetIconImageSKData(unicode, System.Convert.ToInt32(Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width / 3), Xamarin.Forms.Color.Silver, Xamarin.Forms.Color.LightGray))
            {
                using (var output = System.IO.File.OpenWrite(imageFilePath))
                {
                    imageSKData.SaveTo(output);
                }
            }

            return Xamarin.Forms.ImageSource.FromFile(imageFilePath);
        }

        private SkiaSharp.SKData GetIconImageSKData(string unicode, int size, Xamarin.Forms.Color foregroundColor, Xamarin.Forms.Color backgroundColor)
        {
            using (var surface = SkiaSharp.SKSurface.Create(new SkiaSharp.SKImageInfo(size, size, SkiaSharp.SKImageInfo.PlatformColorType, SkiaSharp.SKAlphaType.Premul)))
            {
                surface.Canvas.Clear(backgroundColor.ToSKColor());

                using (var fontFileStream = GetType().Assembly.GetManifestResourceStream("SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Resources.Fonts.IconmonstrIconic.ttf"))
                {
                    using (var paint = new SkiaSharp.SKPaint())
                    {
                        paint.Typeface = SkiaSharp.SKTypeface.FromStream(fontFileStream);
                        paint.TextSize = size / 2;
                        paint.IsAntialias = true;
                        paint.TextAlign = SkiaSharp.SKTextAlign.Center;
                        paint.Color = foregroundColor.ToSKColor();
                        paint.Style = SkiaSharp.SKPaintStyle.Fill;

                        surface.Canvas.DrawText(unicode, size / 2, size / 2 + (paint.TextSize / 2), paint);
                    }

                    using (var skImage = surface.Snapshot())
                    {
                        return skImage.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, 75);
                    }
                }
            }
        }
    }
}
