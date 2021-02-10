using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls.Slide
{
    /// <summary>
    /// Interaction logic for TrailerSlideControl.xaml
    /// </summary>
    public partial class TrailerSlideControl : UserControl, Presenters.Control.IPresentableControl
    {
        private Vlc.DotNet.Wpf.VlcVideoSourceProvider videoSourceProvider;

        public TrailerSlideControl(System.Uri uri)
        {
            InitializeComponent();

            var options = new string[]
            {
                "-I",
                "--dummy-quiet",
                "--ignore-config",
                "--no-video-title",
                "--no-sub-autodetect-file"
            };

            videoSourceProvider = new Vlc.DotNet.Wpf.VlcVideoSourceProvider(Dispatcher);

            videoSourceProvider.CreatePlayer(new System.IO.DirectoryInfo(System.IO.Path.Combine(System.Environment.CurrentDirectory, "libvlc", System.IntPtr.Size == 4 ? "win-x86" : "win-x64")), options);

            videoSourceProvider.MediaPlayer.Playing += AsyncOnMainVideoPlayerStarted;
            videoSourceProvider.MediaPlayer.EncounteredError += AsyncOnMainVideoPlayerVideoPlaybackTerminated;
            videoSourceProvider.MediaPlayer.Stopped += AsyncOnMainVideoPlayerVideoPlaybackTerminated;

            videoSourceProvider.MediaPlayer.Play(uri, options);

            MainVideoPlayerSurfaceImage.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding(nameof(Vlc.DotNet.Wpf.VlcVideoSourceProvider.VideoSource)) { Source = videoSourceProvider });
            MainVideoPlayerSurfaceImage.Stretch = System.Windows.Media.Stretch.UniformToFill;
        }

        public System.EventHandler PresentationShowInEventHandler { get; set; }
        public System.EventHandler PresentationEndedEventHandler { get; set; }

        public System.TimeSpan RequiredExternalDelayTime => System.TimeSpan.Zero;

        public bool RequireFullScreen => true;

        private void AsyncOnMainVideoPlayerStarted(object sender, System.EventArgs eventArgs)
        {
            videoSourceProvider.MediaPlayer.Playing -= AsyncOnMainVideoPlayerStarted;

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                PerformShowInAnimation();
            });
        }

        private void AsyncOnMainVideoPlayerVideoPlaybackTerminated(object sender, System.EventArgs eventArgs)
        {
            videoSourceProvider.MediaPlayer.EncounteredError -= AsyncOnMainVideoPlayerVideoPlaybackTerminated;
            videoSourceProvider.MediaPlayer.Stopped -= AsyncOnMainVideoPlayerVideoPlaybackTerminated;

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                PresentationEndedEventHandler(this, System.EventArgs.Empty);
            });
        }

        private void PerformShowInAnimation()
        {
            Canvas.SetTop(MainVideoPlayerSurfaceImage, ActualHeight);

            var startingBillboardStoryboard = new Storyboard();

            var animation = new DoubleAnimation(0, System.TimeSpan.FromSeconds(1))
            {
                EasingFunction = new QuarticEase { }
            };

            Storyboard.SetTargetName(animation, MainVideoPlayerSurfaceImage.Name);
            Storyboard.SetTargetProperty(animation, new System.Windows.PropertyPath(Canvas.TopProperty));

            startingBillboardStoryboard.Children.Add(animation);

            startingBillboardStoryboard.Completed += PresentationShowInEventHandler;

            startingBillboardStoryboard.Begin(this);
        }
    }
}
