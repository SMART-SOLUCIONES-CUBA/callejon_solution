using SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls.Slide
{
    /// <summary>
    /// Interaction logic for RoomDayBillboardOverviewSlideControl.xaml
    /// </summary>
    public partial class RoomDayBillboardOverviewSlideControl : UserControl, Presenters.Control.IPresentableControl
    {
        private List<PresentationOverviewFrameControl> frames;

        public RoomDayBillboardOverviewSlideControl(Business.Billboard.Cinema.Web.Api.Common.Models.RoomDayBillboardModel roomDayBillboardModel, string cinemaName, string roomName)
        {
            InitializeComponent();

            DateMonthTextBlock.Text = roomDayBillboardModel.Date.ToString("MMMM");
            DateNumericDayTextBlock.Text = roomDayBillboardModel.Date.ToString("dd");
            DateLiteralDayTextBlock.Text = roomDayBillboardModel.Date.ToString("dddd");

            CinemaNameTextBlock.Text = cinemaName;
            RoomNameTextBlock.Text = "Sala " + roomName;

            frames = roomDayBillboardModel.Presentations.Select(item => new PresentationOverviewFrameControl(item)).ToList();
        }

        public EventHandler PresentationShowInEventHandler { get; set; }
        public EventHandler PresentationEndedEventHandler { get; set; }

        public TimeSpan RequiredExternalDelayTime => TimeSpan.FromSeconds(Providers.CachedDataProvider.GetSingleInstance().Configuration.DailyRoomBillboardDisplayTime);

        public bool RequireFullScreen => false;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WrapContainer.Width = ActualWidth;
            WrapContainer.Height = ActualHeight;

            PresentationOverviewsContainer.RowDefinitions.Add(new RowDefinition());

            var limit = Math.Min(Providers.CachedDataProvider.GetSingleInstance().Configuration.DailyRoomBillboardItemMaxCount, frames.Count);

            for (int i = 0; i < limit; i++)
            {
                PresentationOverviewsContainer.ColumnDefinitions.Add(new ColumnDefinition());
            }

            var counter = 0;

            while (counter < limit)
            {
                var frame = frames.ElementAt(counter);

                frame.Width = ActualWidth / Providers.CachedDataProvider.GetSingleInstance().Configuration.DailyRoomBillboardItemMaxCount;

                Grid.SetRow(frame, 0);
                Grid.SetColumn(frame, counter);

                PresentationOverviewsContainer.Children.Add(frame);

                counter++;
            }

            PerformShowInAnimation();
        }

        private void PerformShowInAnimation()
        {
            const string WHITE_OPAQUE_RECTANGLE_NAME = "WhiteOpaqueRectable";
            const string COLOR_OPAQUE_RECTANGLE_NAME = "ColorOpaqueRectable";
            const string COLOR_SOLID_RECTANGLE_NAME = "ColorSolidRectable";

            var beginPosition = ActualWidth * -1;

            var layerColor = Color.FromRgb(0, 24, 49);

            var whiteOpaqueRectangle = new Rectangle { Width = ActualWidth, Height = ActualHeight };

            whiteOpaqueRectangle.Fill = new SolidColorBrush(Colors.White);
            whiteOpaqueRectangle.Opacity = 0.75;

            Canvas.SetLeft(whiteOpaqueRectangle, beginPosition);

            RegisterName(WHITE_OPAQUE_RECTANGLE_NAME, whiteOpaqueRectangle);

            ForegroundAnimationContainer.Children.Add(whiteOpaqueRectangle);

            var colorOpaqueRectangle = new Rectangle { Width = ActualWidth, Height = ActualHeight };

            colorOpaqueRectangle.Fill = new SolidColorBrush(layerColor);
            colorOpaqueRectangle.Opacity = 0.75;

            Canvas.SetLeft(colorOpaqueRectangle, beginPosition);

            RegisterName(COLOR_OPAQUE_RECTANGLE_NAME, colorOpaqueRectangle);

            ForegroundAnimationContainer.Children.Add(colorOpaqueRectangle);

            var colorSolidRectangle = new Rectangle { Width = ActualWidth, Height = ActualHeight };

            colorSolidRectangle.Fill = new SolidColorBrush(layerColor);

            Canvas.SetLeft(colorSolidRectangle, beginPosition);

            RegisterName(COLOR_SOLID_RECTANGLE_NAME, colorSolidRectangle);

            ForegroundAnimationContainer.Children.Add(colorSolidRectangle);

            WrapContainer.RenderTransform = new RotateTransform();

            Canvas.SetLeft(WrapContainer, beginPosition * -1);

            var storyboard = new Storyboard();

            var whiteOpaqueRectangleInOutAnimation = new DoubleAnimationUsingKeyFrames()
            {
                Duration = TimeSpan.FromSeconds(2.4)
            };

            whiteOpaqueRectangleInOutAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2)), new QuarticEase()));
            whiteOpaqueRectangleInOutAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(beginPosition, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.4)), new QuarticEase()));

            Storyboard.SetTargetName(whiteOpaqueRectangleInOutAnimation, WHITE_OPAQUE_RECTANGLE_NAME);
            Storyboard.SetTargetProperty(whiteOpaqueRectangleInOutAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(whiteOpaqueRectangleInOutAnimation);

            var colorOpaqueRectangleInOutAnimation = new DoubleAnimationUsingKeyFrames()
            {
                Duration = TimeSpan.FromSeconds(2),
                BeginTime = TimeSpan.FromSeconds(0.1)
            };

            colorOpaqueRectangleInOutAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)), new QuarticEase()));
            colorOpaqueRectangleInOutAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(beginPosition, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2)), new QuarticEase()));

            Storyboard.SetTargetName(colorOpaqueRectangleInOutAnimation, COLOR_OPAQUE_RECTANGLE_NAME);
            Storyboard.SetTargetProperty(colorOpaqueRectangleInOutAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(colorOpaqueRectangleInOutAnimation);

            var colorSolidRectangleInOutAnimation = new DoubleAnimationUsingKeyFrames()
            {
                Duration = TimeSpan.FromSeconds(1.6),
                BeginTime = TimeSpan.FromSeconds(0.2)
            };

            colorSolidRectangleInOutAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8)), new QuarticEase()));
            colorSolidRectangleInOutAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(beginPosition, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.6)), new QuarticEase()));

            Storyboard.SetTargetName(colorSolidRectangleInOutAnimation, COLOR_SOLID_RECTANGLE_NAME);
            Storyboard.SetTargetProperty(colorSolidRectangleInOutAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(colorSolidRectangleInOutAnimation);

            var wrapContainerAppearAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(1))
            {
                BeginTime = TimeSpan.FromSeconds(1.2),
                EasingFunction = new QuarticEase { },
            };

            Storyboard.SetTargetName(wrapContainerAppearAnimation, WrapContainer.Name);
            Storyboard.SetTargetProperty(wrapContainerAppearAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(wrapContainerAppearAnimation);

            storyboard.Completed += OnPresentationStarted;

            storyboard.Begin(this);
        }

        private void OnPresentationStarted(object sender, EventArgs e)
        {
            ForegroundAnimationContainer.Children.Clear();

            PresentationShowInEventHandler(this, e);
        }
    }
}
