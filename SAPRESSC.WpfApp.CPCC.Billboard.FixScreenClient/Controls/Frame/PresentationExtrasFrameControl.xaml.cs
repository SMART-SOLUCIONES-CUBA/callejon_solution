using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using System.Windows.Controls;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls.Frame
{
    /// <summary>
    /// Interaction logic for PresentationExtrasFrameControl.xaml
    /// </summary>
    public partial class PresentationExtrasFrameControl : UserControl
    {
        public PresentationExtrasFrameControl(AwardWinningModel[] awardWinnings)
        {
            InitializeComponent();

            for (int i = 0; i < awardWinnings.Length; i++)
            {
                MainContainer.RowDefinitions.Add(new RowDefinition { Height = System.Windows.GridLength.Auto });
            }

            MainContainer.RowDefinitions.Add(new RowDefinition());
            MainContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = System.Windows.GridLength.Auto });
            MainContainer.ColumnDefinitions.Add(new ColumnDefinition());

            var index = 0;

            foreach (var item in awardWinnings)
            {
                var iconTextBlock = new TextBlock
                {
                    Text = "\ue0dd",
                    FontFamily = new System.Windows.Media.FontFamily("IconmonstrIconic.ttf#iconmonstr-iconic-font"),
                    FontSize = 24,
                    Margin = new System.Windows.Thickness(10)
                };

                MainContainer.Children.Add(iconTextBlock);

                Grid.SetRow(iconTextBlock, index);
                Grid.SetColumn(iconTextBlock, 0);

                var textBlock = new TextBlock
                {
                    Text = string.Format("{0} Premios {1} {2}, {3}", item.Type == 0 ? "Ganador" : "Nominado", item.Award.Name, item.Edition, item.Category),
                    TextWrapping = System.Windows.TextWrapping.Wrap,
                    FontSize = 27,
                    FontFamily = new System.Windows.Media.FontFamily("Helvetica LT Std"),
                    FontStretch = System.Windows.FontStretches.Condensed,
                    Margin = new System.Windows.Thickness(0, 0, 0, 10)
                };

                MainContainer.Children.Add(textBlock);

                Grid.SetRow(textBlock, index++);
                Grid.SetColumn(textBlock, 1);
            }
        }
    }
}
