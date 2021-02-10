using System.Windows;
using System.Windows.Controls;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls.Frame
{
    /// <summary>
    /// Interaction logic for BillboardSidebarControl.xaml
    /// </summary>
    public partial class BillboardSidebarFrameControl : UserControl
    {
        public System.DateTime TargetDate { get; set; }

        public BillboardSidebarFrameControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var spanichCultureInfo = System.Globalization.CultureInfo.GetCultureInfo("es");

            DateMonth.Text = TargetDate.ToString("MMMM", spanichCultureInfo);
            DateNumericDay.Text = TargetDate.ToString("dd");
            DateLiteralDay.Text = TargetDate.ToString("dddd", spanichCultureInfo);

            var playlistStatus = Providers.PlaylistProvider.GetSingleInstance().Status;

            var playlistStatusItemsLength = playlistStatus.Items.Length;

            for (int i = 0; i < playlistStatusItemsLength; i++)
            {
                var item = playlistStatus.Items[i];

                var overviewControl = new PlaylistItemOverviewFrameControl();

                if (playlistStatus.CurrentIndex != i)
                {
                    overviewControl.WrapContainer.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                }

                if (i < playlistStatus.CurrentIndex)
                {
                    overviewControl.WrapContainer.Opacity = 0.5;
                }

                overviewControl.TitleTextBlock.Text = item.Title;
                overviewControl.TagTextBlock.Text = item.Tag;

                if (item.Subjects.Length > 1)
                {
                    overviewControl.PrimaryLineTextBlock.Text = item.Subjects[0];
                    overviewControl.SecundaryLineTextBlock.Text = item.Subjects[1];
                }
                else
                {
                    overviewControl.PrimaryLineTextBlock.Text = item.Subjects[0];
                    overviewControl.SecundaryLineTextBlock.Text = item.About;
                }

                PlaylistStatusItemsContainer.Children.Add(overviewControl);
            }
        }
    }
}
