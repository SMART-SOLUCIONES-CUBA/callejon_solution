using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();

            if (Screen.AllScreens.Length > 1)
            {
                var secundaryScreen = Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();

                if (secundaryScreen != null)
                {
                    mainWindow.Top = secundaryScreen.WorkingArea.Top;
                    mainWindow.Left = secundaryScreen.WorkingArea.Left;
                }
            }

            mainWindow.Show();
        }
    }
}
