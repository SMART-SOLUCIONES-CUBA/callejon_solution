using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls
{
    /// <summary>
    /// Interaction logic for ContentFrameCarouselControl.xaml
    /// </summary>
    public partial class ContentFrameCarouselControl : UserControl
    {
        private Queue<UserControl> frames;

        private TimeSpan elapse;

        private bool animateIn;
        private int currentFrameIndex;

        public ContentFrameCarouselControl()
        {
            InitializeComponent();

            animateIn = false;
        }

        public void Start(Queue<UserControl> frames, TimeSpan elapse)
        {
            this.frames = frames;
            this.elapse = elapse;

            currentFrameIndex = 0;

            Indicator3.Visibility = frames.Count > 2 ? Visibility.Visible : Visibility.Collapsed;

            Task.Factory.StartNew(() => ShowFrame());
        }

        private void ShowFrame()
        {
            Dispatcher.Invoke(delegate
            {
                if (ContentPresenter.Content != null)
                {
                    if (ContentPresenter.Content is UserControl oldFrame)
                    {
                        oldFrame.Loaded -= CarouselFrame_Loaded;

                        UnloadFrame(oldFrame);
                    }
                }
                else
                {
                    ShowNextFrame();
                }
            });
        }

        private void ShowNextFrame()
        {
            var newFrame = frames.Dequeue();

            currentFrameIndex++;

            if (animateIn)
            {
                newFrame.Loaded += CarouselFrame_Loaded;

                var formerIndicator = FindName("Indicator" + (currentFrameIndex - 1)) as Border;

                var currentIndicator = FindName("Indicator" + currentFrameIndex) as Border;

                var tempIndicatorBackground = currentIndicator.Background;

                currentIndicator.Background = formerIndicator.Background;

                formerIndicator.Background = tempIndicatorBackground;
            }

            ContentPresenter.Content = newFrame;

            if (!animateIn) animateIn = true;

            if (frames.Count <= 0) return;

            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = elapse
            };

            timer.Tick += new EventHandler(OnElapseTimerTick);

            timer.Start();
        }
        private void OnElapseTimerTick(object sender, EventArgs e)
        {
            var timer = sender as System.Windows.Threading.DispatcherTimer;

            ShowFrame();

            timer.Stop();
        }

        private void UnloadFrame(UserControl slide)
        {
            var outTransitionStoryboard = (Resources["TransitionOutStoryboard"] as System.Windows.Media.Animation.Storyboard).Clone();

            outTransitionStoryboard.Completed += HideFrame_Completed;

            outTransitionStoryboard.Begin(ContentPresenter);
        }

        private void CarouselFrame_Loaded(object sender, RoutedEventArgs e)
        {
            var intTransitionStoryboard = Resources["TransitionInStoryboard"] as System.Windows.Media.Animation.Storyboard;

            intTransitionStoryboard.Begin(ContentPresenter);
        }

        private void HideFrame_Completed(object sender, EventArgs e)
        {
            ContentPresenter.Content = null;

            ShowNextFrame();
        }
    }
}
