using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls.Frame
{
    /// <summary>
    /// Interaction logic for PresentationOverviewFrameControl.xaml
    /// </summary>
    public partial class PresentationOverviewFrameControl : UserControl
    {
        public PresentationOverviewFrameControl(Business.Billboard.Cinema.Web.Api.Common.Models.PresentationModel presentation)
        {
            InitializeComponent();

            TitleTextBlock.Text = presentation.Production.Title;
            TimesTextBlock.Text = presentation.Date.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture);

            CountryTextBlock.Text = presentation.Production.Country.Name;
            YearTextBlock.Text = presentation.Production.Year.ToString();

            var imageSource = new BitmapImage();

            using (var data = new System.IO.MemoryStream(presentation.Production.PosterImage.CachedData))
            {
                data.Position = 0;

                imageSource.BeginInit();
                imageSource.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.UriSource = null;
                imageSource.StreamSource = data;
                imageSource.EndInit();
            }

            imageSource.Freeze();

            PosterImage.Source = imageSource;
        }
    }
}
