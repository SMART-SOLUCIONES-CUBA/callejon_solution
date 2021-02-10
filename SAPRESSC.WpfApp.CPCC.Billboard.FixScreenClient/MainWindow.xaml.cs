using System.Windows;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Presenters.MainPresenter presenter;

        public MainWindow()
        {
            InitializeComponent();

            presenter = new Presenters.MainPresenter(PersistentContainer, TransitionContainer);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;

            presenter.InitPresentation();
        }
    }
}