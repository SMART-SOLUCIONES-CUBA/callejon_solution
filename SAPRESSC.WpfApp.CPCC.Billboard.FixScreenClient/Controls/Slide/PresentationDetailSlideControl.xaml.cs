using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls.Frame;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PresentationDetailSlideControl.xaml
    /// </summary>
    public partial class PresentationDetailSlideControl : UserControl, Presenters.Control.IPresentableControl
    {
        private enum ShowInAngleAnimationType
        {
            Left,
            Right
        }

        private PresentationModel presentationModel;
        private bool animateShowIn;

        public PresentationDetailSlideControl(bool animateShowIn, PresentationModel presentationModel, string cinemaName, string roomName)
        {
            this.presentationModel = presentationModel;
            this.animateShowIn = animateShowIn;

            InitializeComponent();

            BillboardSidebar.TargetDate = presentationModel.Date;

            var imageSource = new BitmapImage();

            using (var data = new System.IO.MemoryStream(presentationModel.Production.PosterImage.CachedData))
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

            RoomNameTextBlock.Text = "Sala " + roomName;
            CinemaNameTextBlock.Text = cinemaName;
            TimesTextBlock.Text = presentationModel.Date.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            TitleTextBlock.Text = presentationModel.Production.Title;
        }

        public EventHandler PresentationShowInEventHandler { get; set; }
        public EventHandler PresentationEndedEventHandler { get; set; }

        public TimeSpan RequiredExternalDelayTime
        {
            get
            {
                var slideFrameCount = presentationModel.Production.AwardWinnings.Length > 0 ? 3 : 2;

                return TimeSpan.FromSeconds(Providers.CachedDataProvider.GetSingleInstance().Configuration.PresentationDetailDisplayTime * slideFrameCount + slideFrameCount);
            }
        }

        public bool RequireFullScreen => false;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WrapContainer.Width = ActualWidth;
            WrapContainer.Height = ActualHeight;
            WrapContainer.Visibility = Visibility.Hidden;

            BillboardSidebar.Width = ActualWidth / 6.5;
            BillboardSidebar.MaxWidth = BillboardSidebar.Width;
            BillboardSidebar.MinWidth = BillboardSidebar.Width;

            var carouselFrames = new Queue<UserControl>();

            var duration = string.Format("{0} hora {1} minutos", presentationModel.Production.Duration.Hours, presentationModel.Production.Duration.Minutes);

            if (presentationModel.Production.Duration.Hours < 1)
            {
                duration = string.Format("{0} minutos", presentationModel.Production.Duration.Minutes);
            }
            else if (presentationModel.Production.Duration.Hours > 1)
            {
                duration = string.Format("{0} horas {1} minutos", presentationModel.Production.Duration.Hours, presentationModel.Production.Duration.Minutes);
            }

            var featuresFrame = new PresentationFeaturesFrameControl();

            featuresFrame.DirectionTextBlock.Text = string.Join(", ", presentationModel.Production.Directors.Select(item => item.FullName));
            featuresFrame.CountryTextBlock.Text = presentationModel.Production.Country.Name;
            featuresFrame.YearTextBlock.Text = presentationModel.Production.Year.ToString();
            featuresFrame.DurationTextBlock.Text = duration;
            featuresFrame.GenresTextBlock.Text = string.Join(", ", presentationModel.Production.Genres.Select(genre => genre.Name));
            featuresFrame.CategoryTextBlock.Text = presentationModel.Production.Clasification.Name;

            if (presentationModel.Production.Interpreters.Length > 0)
            {
                featuresFrame.InterpretationsTextBlock.Text = string.Join(", ", presentationModel.Production.Interpreters.Select(item => item.FullName));
            }
            else
            {
                featuresFrame.InterpretationsWrapTextBlock.Visibility = Visibility.Hidden;
            }

            carouselFrames.Enqueue(featuresFrame);

            var aboutFrame = new PresentationAboutFrameControl();

            aboutFrame.AboutTextBlock.Text = presentationModel.Production.About;

            carouselFrames.Enqueue(aboutFrame);

            if (presentationModel.Production.AwardWinnings.Length > 0)
            {
                carouselFrames.Enqueue(new PresentationExtrasFrameControl(presentationModel.Production.AwardWinnings));
            }

            ContentFrameCarousel.Start(carouselFrames, TimeSpan.FromSeconds(Providers.CachedDataProvider.GetSingleInstance().Configuration.PresentationDetailDisplayTime));

            PerformShowInAnimation();
        }

        private void PopulateAngleTransition(Storyboard storyboard, string targetName, TimeSpan delay, ShowInAngleAnimationType type, EventHandler completeEventHandler = null)
        {
            var duration = TimeSpan.FromSeconds(2);

            var moveXAnimation = new DoubleAnimation(type == ShowInAngleAnimationType.Left ? ActualHeight * -1 : ActualHeight, duration)
            {
                BeginTime = delay,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTargetName(moveXAnimation, targetName);
            Storyboard.SetTargetProperty(moveXAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(moveXAnimation);

            var moveYAnimation = new DoubleAnimation(type == ShowInAngleAnimationType.Left ? ActualWidth / 2 : ActualWidth / 2 * -1, duration)
            {
                BeginTime = delay,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTargetName(moveYAnimation, targetName);
            Storyboard.SetTargetProperty(moveYAnimation, new PropertyPath(Canvas.TopProperty));

            storyboard.Children.Add(moveYAnimation);

            var rotateAnimation = new DoubleAnimation(type == ShowInAngleAnimationType.Left ? 45 : 45 * -1, duration)
            {
                BeginTime = delay,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTargetName(rotateAnimation, targetName);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

            if (completeEventHandler != null) rotateAnimation.Completed += completeEventHandler;

            storyboard.Children.Add(rotateAnimation);
        }

        private void PopulateDownTransition(Storyboard storyboard, string targetName, TimeSpan delay)
        {
            var animation = new DoubleAnimation(0, TimeSpan.FromSeconds(1))
            {
                BeginTime = delay,
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTargetName(animation, targetName);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));

            storyboard.Children.Add(animation);
        }

        private void PerformShowInAnimation()
        {
            const string LEFT_WHITE_OPAQUE_RECTANGLE_NAME = "LeftWhiteOpaqueRectable";
            const string LEFT_COLOR_OPAQUE_BACKGROUND_RECTANGLE_NAME = "LeftColorOpaqueBackgroundRectable";
            const string LEFT_COLOR_SOLID_RECTANGLE_NAME = "LeftColorSolidRectable";
            const string RIGHT_WHITE_OPAQUE_RECTANGLE_NAME = "RightWhiteOpaqueRectable";
            const string RIGHT_COLOR_OPAQUE_BACKGROUND_RECTANGLE_NAME = "RightColorOpaqueBackgroundRectable";
            const string RIGHT_COLOR_SOLID_RECTANGLE_NAME = "RightColorSolidRectable";

            var layerColor = Color.FromRgb(0, 24, 49);

            var leftWhiteOpaqueRectangle = new Rectangle { Width = ActualWidth / 2, Height = ActualHeight };

            leftWhiteOpaqueRectangle.Fill = new SolidColorBrush(Colors.White);
            leftWhiteOpaqueRectangle.Opacity = 0.75;
            leftWhiteOpaqueRectangle.RenderTransform = new RotateTransform {  };

            Canvas.SetLeft(leftWhiteOpaqueRectangle, 0);
            Canvas.SetTop(leftWhiteOpaqueRectangle, ActualHeight * -1);

            RegisterName(LEFT_WHITE_OPAQUE_RECTANGLE_NAME, leftWhiteOpaqueRectangle);

            ForegroundAnimationContainer.Children.Add(leftWhiteOpaqueRectangle);

            var rightWhiteOpaqueRectangle = new Rectangle { Width = ActualWidth / 2, Height = ActualHeight };

            rightWhiteOpaqueRectangle.Fill = new SolidColorBrush(Colors.White);
            rightWhiteOpaqueRectangle.Opacity = 0.75;
            rightWhiteOpaqueRectangle.RenderTransform = new RotateTransform { };

            Canvas.SetLeft(rightWhiteOpaqueRectangle, ActualWidth / 2);
            Canvas.SetTop(rightWhiteOpaqueRectangle, ActualHeight);

            RegisterName(RIGHT_WHITE_OPAQUE_RECTANGLE_NAME, rightWhiteOpaqueRectangle);

            ForegroundAnimationContainer.Children.Add(rightWhiteOpaqueRectangle);

            var leftColorOpaqueBackgroudRectangle = new Rectangle { Width = ActualWidth / 2, Height = ActualHeight };

            leftColorOpaqueBackgroudRectangle.Fill = new SolidColorBrush(layerColor);
            leftColorOpaqueBackgroudRectangle.Opacity = 0.75;
            leftColorOpaqueBackgroudRectangle.RenderTransform = new RotateTransform { };

            Canvas.SetLeft(leftColorOpaqueBackgroudRectangle, 0);
            Canvas.SetTop(leftColorOpaqueBackgroudRectangle, ActualHeight * -1);

            RegisterName(LEFT_COLOR_OPAQUE_BACKGROUND_RECTANGLE_NAME, leftColorOpaqueBackgroudRectangle);

            ForegroundAnimationContainer.Children.Add(leftColorOpaqueBackgroudRectangle);

            var rightColorOpaqueBackgroudRectangle = new Rectangle { Width = ActualWidth / 2, Height = ActualHeight };

            rightColorOpaqueBackgroudRectangle.Fill = new SolidColorBrush(layerColor);
            rightColorOpaqueBackgroudRectangle.Opacity = 0.75;
            rightColorOpaqueBackgroudRectangle.RenderTransform = new RotateTransform { };

            Canvas.SetLeft(rightColorOpaqueBackgroudRectangle, ActualWidth / 2);
            Canvas.SetTop(rightColorOpaqueBackgroudRectangle, ActualHeight);

            RegisterName(RIGHT_COLOR_OPAQUE_BACKGROUND_RECTANGLE_NAME, rightColorOpaqueBackgroudRectangle);

            ForegroundAnimationContainer.Children.Add(rightColorOpaqueBackgroudRectangle);

            var leftColorSolidRectangle = new Rectangle { Width = ActualWidth / 2, Height = ActualHeight };

            leftColorSolidRectangle.Fill = new SolidColorBrush(layerColor);
            leftColorSolidRectangle.RenderTransform = new RotateTransform { };

            Canvas.SetLeft(leftColorSolidRectangle, 0);
            Canvas.SetTop(leftColorSolidRectangle, ActualHeight * -1);

            RegisterName(LEFT_COLOR_SOLID_RECTANGLE_NAME, leftColorSolidRectangle);

            ForegroundAnimationContainer.Children.Add(leftColorSolidRectangle);

            var rightColorSolidRectangle = new Rectangle { Width = ActualWidth / 2, Height = ActualHeight };

            rightColorSolidRectangle.Fill = new SolidColorBrush(layerColor);
            rightColorSolidRectangle.RenderTransform = new RotateTransform { };

            Canvas.SetLeft(rightColorSolidRectangle, ActualWidth / 2);
            Canvas.SetTop(rightColorSolidRectangle, ActualHeight);

            RegisterName(RIGHT_COLOR_SOLID_RECTANGLE_NAME, rightColorSolidRectangle);

            ForegroundAnimationContainer.Children.Add(rightColorSolidRectangle);

            var beginStorybard = new Storyboard();

            PopulateDownTransition(beginStorybard, LEFT_WHITE_OPAQUE_RECTANGLE_NAME, TimeSpan.Zero);
            PopulateDownTransition(beginStorybard, RIGHT_WHITE_OPAQUE_RECTANGLE_NAME, TimeSpan.Zero);
            PopulateDownTransition(beginStorybard, LEFT_COLOR_OPAQUE_BACKGROUND_RECTANGLE_NAME, TimeSpan.FromMilliseconds(200));
            PopulateDownTransition(beginStorybard, RIGHT_COLOR_OPAQUE_BACKGROUND_RECTANGLE_NAME, TimeSpan.FromMilliseconds(200));
            PopulateDownTransition(beginStorybard, LEFT_COLOR_SOLID_RECTANGLE_NAME, TimeSpan.FromMilliseconds(400));
            PopulateDownTransition(beginStorybard, RIGHT_COLOR_SOLID_RECTANGLE_NAME, TimeSpan.FromMilliseconds(400));

            beginStorybard.Completed += (sender, e) =>
            {
                WrapContainer.Visibility = Visibility.Visible;

                var endStoryboard = new Storyboard();

                PopulateAngleTransition(endStoryboard, LEFT_COLOR_SOLID_RECTANGLE_NAME, TimeSpan.Zero, ShowInAngleAnimationType.Left);
                PopulateAngleTransition(endStoryboard, RIGHT_COLOR_SOLID_RECTANGLE_NAME, TimeSpan.Zero, ShowInAngleAnimationType.Right);
                PopulateAngleTransition(endStoryboard, LEFT_COLOR_OPAQUE_BACKGROUND_RECTANGLE_NAME, TimeSpan.FromMilliseconds(100), ShowInAngleAnimationType.Left);
                PopulateAngleTransition(endStoryboard, RIGHT_COLOR_OPAQUE_BACKGROUND_RECTANGLE_NAME, TimeSpan.FromMilliseconds(100), ShowInAngleAnimationType.Right);
                PopulateAngleTransition(endStoryboard, LEFT_WHITE_OPAQUE_RECTANGLE_NAME, TimeSpan.FromMilliseconds(200), ShowInAngleAnimationType.Left);
                PopulateAngleTransition(endStoryboard, RIGHT_WHITE_OPAQUE_RECTANGLE_NAME, TimeSpan.FromMilliseconds(200), ShowInAngleAnimationType.Right);

                endStoryboard.Completed += OnPresentationStarted;

                endStoryboard.Begin(this);
            };

            beginStorybard.Begin(this);
        }

        private void OnPresentationStarted(object sender, EventArgs e)
        {
            ForegroundAnimationContainer.Children.Clear();

            PresentationShowInEventHandler(this, e);
        }
    }
}
