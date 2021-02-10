using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls.Slide
{
    /// <summary>
    /// Interaction logic for CinemaOverviewSlideControl.xaml
    /// </summary>
    public partial class CinemaOverviewSlideControl : UserControl, Presenters.Control.IPresentableControl
    {
        private bool animateShowIn;

        public CinemaOverviewSlideControl(Business.Billboard.Cinema.Web.Api.Common.Models.CinemaModel cinemaModel, bool animateShowIn)
        {
            InitializeComponent();

            this.animateShowIn = animateShowIn;

            NameTextBlock.Text = cinemaModel.Name;
            AddressTextBlock.Text = cinemaModel.Addresses.First().Address;

            if (cinemaModel.Image != null)
            {
                var imageSource = new BitmapImage();

                using (var data = new System.IO.MemoryStream(cinemaModel.Image.CachedData))
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

                OverviewImage.Source = imageSource;
            }
        }

        public EventHandler PresentationShowInEventHandler { get; set; }
        public EventHandler PresentationEndedEventHandler { get; set; }

        public TimeSpan RequiredExternalDelayTime => TimeSpan.FromSeconds(Providers.CachedDataProvider.GetSingleInstance().Configuration.CinemaOverviewDisplayTime);

        public bool RequireFullScreen => false;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ImageContainer.Width = ActualWidth;
            ImageContainer.Height = ActualHeight;

            InformationContainer.Width = ActualWidth;
            InformationContainer.Height = ActualHeight;

            InformationShadownContainer.Width = ActualWidth;
            InformationShadownContainer.Height = ActualHeight;

            if (animateShowIn)
            {
                PerformShowInAnimation();
            }
            else
            {
                PresentationShowInEventHandler(this, new EventArgs());
            }
        }

        private void PopulateAngleTransition(Storyboard storyboard, string targetName, TimeSpan delay, EventHandler completeEventHandler = null)
        {
            var duration = TimeSpan.FromSeconds(1);

            var moveXAnimation = new DoubleAnimation(0, duration)
            {
                BeginTime = delay,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTargetName(moveXAnimation, targetName);
            Storyboard.SetTargetProperty(moveXAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(moveXAnimation);

            var moveYAnimation = new DoubleAnimation(ActualHeight * -1, 0, duration)
            {
                BeginTime = delay,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTargetName(moveYAnimation, targetName);
            Storyboard.SetTargetProperty(moveYAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(moveYAnimation);

            var rotateAnimation = new DoubleAnimation(45 * -1, 0, duration)
            {
                BeginTime = delay,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTargetName(rotateAnimation, targetName);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

            if (completeEventHandler != null) rotateAnimation.Completed += completeEventHandler;

            storyboard.Children.Add(rotateAnimation);
        }

        private void PerformShowInAnimation()
        {
            const string WHITE_OPAQUE_RECTANGLE_NAME = "WhiteOpaqueRectable";
            const string RED_OPAQUE_BACKGROUND_RECTANGLE_NAME = "RedOpaqueBackgroundRectable";
            const string RED_SOLID_RECTANGLE_NAME = "RedSolidRectable";

            var layerColor = Color.FromRgb(0, 24, 49);

            var whiteOpaqueRectangle = new Rectangle { Width = ActualWidth, Height = ActualHeight };

            whiteOpaqueRectangle.Fill = new SolidColorBrush(Colors.White);
            whiteOpaqueRectangle.Opacity = 0.5;
            whiteOpaqueRectangle.RenderTransform = new RotateTransform { };

            Canvas.SetLeft(whiteOpaqueRectangle, ActualWidth * -1);

            RegisterName(WHITE_OPAQUE_RECTANGLE_NAME, whiteOpaqueRectangle);

            BackgroundAnimationContainer.Children.Add(whiteOpaqueRectangle);

            var redOpaqueBackgroudRectangle = new Rectangle { Width = ActualWidth, Height = ActualHeight };

            redOpaqueBackgroudRectangle.Fill = new SolidColorBrush(layerColor);
            redOpaqueBackgroudRectangle.Opacity = 0.75;
            redOpaqueBackgroudRectangle.RenderTransform = new RotateTransform { };

            Canvas.SetLeft(redOpaqueBackgroudRectangle, ActualWidth);

            RegisterName(RED_OPAQUE_BACKGROUND_RECTANGLE_NAME, redOpaqueBackgroudRectangle);

            BackgroundAnimationContainer.Children.Add(redOpaqueBackgroudRectangle);

            var redSolidRectangle = new Rectangle { Width = ActualWidth, Height = ActualHeight };

            redSolidRectangle.Fill = new SolidColorBrush(layerColor);
            redSolidRectangle.RenderTransform = new RotateTransform { };

            Canvas.SetLeft(redSolidRectangle, ActualWidth * -1);

            RegisterName(RED_SOLID_RECTANGLE_NAME, redSolidRectangle);

            BackgroundAnimationContainer.Children.Add(redSolidRectangle);

            ImageContainer.RenderTransform = new RotateTransform();

            Canvas.SetLeft(ImageContainer, ActualWidth * -1);
            Canvas.SetLeft(InformationShadownContainer, ActualWidth * -1);
            Canvas.SetLeft(InformationContainer, ActualWidth * -1);

            var storyboard = new Storyboard();

            PopulateAngleTransition(storyboard, WHITE_OPAQUE_RECTANGLE_NAME, TimeSpan.Zero);
            PopulateAngleTransition(storyboard, RED_OPAQUE_BACKGROUND_RECTANGLE_NAME, TimeSpan.FromMilliseconds(200));
            PopulateAngleTransition(storyboard, RED_SOLID_RECTANGLE_NAME, TimeSpan.FromMilliseconds(400));
            PopulateAngleTransition(storyboard, ImageContainer.Name, TimeSpan.FromMilliseconds(600));

            var informationShadownAppearAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5))
            {
                BeginTime = TimeSpan.FromSeconds(1.5),
                EasingFunction = new QuarticEase { }
            };

            Storyboard.SetTargetName(informationShadownAppearAnimation, InformationShadownContainer.Name);
            Storyboard.SetTargetProperty(informationShadownAppearAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(informationShadownAppearAnimation);

            var informationAppearAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5))
            {
                BeginTime = TimeSpan.FromSeconds(1.6),
                EasingFunction = new QuarticEase { }
            };

            Storyboard.SetTargetName(informationAppearAnimation, InformationContainer.Name);
            Storyboard.SetTargetProperty(informationAppearAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(informationAppearAnimation);

            storyboard.Completed += OnPresentationStarted;

            storyboard.Begin(this);
        }

        private void OnPresentationStarted(object sender, EventArgs e)
        {
            BackgroundAnimationContainer.Children.Clear();

            PresentationShowInEventHandler(this, e);
        }
    }
}
