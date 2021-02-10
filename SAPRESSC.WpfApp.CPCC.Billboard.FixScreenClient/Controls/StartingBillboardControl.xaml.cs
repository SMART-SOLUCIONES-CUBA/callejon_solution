using SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Providers;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Animation;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls
{
    /// <summary>
    /// Interaction logic for StartingBillboardControl.xaml
    /// </summary>
    public partial class StartingBillboardControl : UserControl, Presenters.Control.IPresentableControl
    {
        private bool startingMode;

        public StartingBillboardControl(bool startingMode)
        {
            InitializeComponent();

            this.startingMode = startingMode;
        }

        public System.EventHandler PresentationShowInEventHandler { get; set; }
        public System.EventHandler PresentationEndedEventHandler { get; set; }

        public System.TimeSpan RequiredExternalDelayTime => throw new System.NotImplementedException();

        public bool RequireFullScreen => false;

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            BackboardImage.Width = ActualWidth;
            BackboardImage.Height = ActualHeight;
            BackboardImage.Source = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.StartingBillboard.GetHbitmap(),
                                      System.IntPtr.Zero,
                                      System.Windows.Int32Rect.Empty,
                                      System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            Canvas.SetTop(BackboardImage, ActualHeight * -1);

            var startingBillboardStoryboard = new Storyboard();

            var animation = new DoubleAnimation(0, System.TimeSpan.FromSeconds(1))
            {
                EasingFunction = new QuarticEase { }
            };

            if (!startingMode)
            {
                animation.EasingFunction = new BounceEase { Bounciness = 10, Bounces = 1 };
            }

            Storyboard.SetTargetName(animation, BackboardImage.Name);
            Storyboard.SetTargetProperty(animation, new System.Windows.PropertyPath(Canvas.TopProperty));

            startingBillboardStoryboard.Children.Add(animation);

            startingBillboardStoryboard.Completed += PresentationShowInEventHandler;

            startingBillboardStoryboard.Begin(this);

            var updateBillboarDataSourceWorker = new System.ComponentModel.BackgroundWorker { WorkerReportsProgress = true };

            updateBillboarDataSourceWorker.DoWork += UpdateBillboarDataSourceWorker_DoWork;
            updateBillboarDataSourceWorker.ProgressChanged += UpdateBillboarDataSourceWorker_ProgressChanged;
            updateBillboarDataSourceWorker.RunWorkerCompleted += UpdateBillboarDataSourceWorker_RunWorkerCompleted;

            updateBillboarDataSourceWorker.RunWorkerAsync();
        }

        private void UpdateBillboarDataSourceWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            WeitingProgressBar.Visibility = System.Windows.Visibility.Visible;
        }

        private void UpdateBillboarDataSourceWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            PlaylistProvider.GetSingleInstance().ReloadData();

            (sender as System.ComponentModel.BackgroundWorker).Dispose();

            PresentationEndedEventHandler(sender, e);
        }

        private void UpdateBillboarDataSourceWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Thread.Sleep(System.TimeSpan.FromSeconds(5));

            while (true)
            {
                while (true)
                {
                    try
                    {
                        CachedDataProvider.GetSingleInstance().UpdateConfiguration();

                        break;
                    }
                    catch
                    {
                        if (CachedDataProvider.GetSingleInstance().Configuration == null)
                        {
                            (sender as System.ComponentModel.BackgroundWorker).ReportProgress(0, "Weiting ...");
                        }

                        Thread.Sleep(System.TimeSpan.FromSeconds(30));
                    }
                }

                try
                {
                    CachedDataProvider.GetSingleInstance().UpdateAdvises();
                }
                catch { }

                try
                {
                    CachedDataProvider.GetSingleInstance().UpdateEvents();
                }
                catch { }

                try
                {
                    CachedDataProvider.GetSingleInstance().UpdateCinemaBillboards();
                }
                catch { }

                if (CachedDataProvider.GetSingleInstance().CinemaBillboards.Count > 0 || CachedDataProvider.GetSingleInstance().Events.Count > 0)
                {
                    break;
                }

                (sender as System.ComponentModel.BackgroundWorker).ReportProgress(0, "Weiting ...");

                Thread.Sleep(System.TimeSpan.FromSeconds(30));
            }

            if (startingMode && CachedDataProvider.GetSingleInstance().Configuration != null && CachedDataProvider.GetSingleInstance().Configuration.StartUpDelayTime > 0)
            {
                Thread.Sleep(System.TimeSpan.FromSeconds(CachedDataProvider.GetSingleInstance().Configuration.StartUpDelayTime));
            }
        }
    }
}
