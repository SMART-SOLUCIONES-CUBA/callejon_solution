using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System;
using SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Providers;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls.Slide
{
    /// <summary>
    /// Interaction logic for EventDetailSlideControl.xaml
    /// </summary>
    public partial class EventDetailSlideControl : UserControl, Presenters.Control.IPresentableControl
    {
        private bool animateShowIn;

        public EventDetailSlideControl(EventModel eventModel, bool animateShowIn)
        {
            this.animateShowIn = animateShowIn;

            InitializeComponent();

            BillboardSidebar.TargetDate = eventModel.Start;

            var imageSource = new BitmapImage();

            HeaderTextBlock.Text = eventModel.Header;
            TitleTextBlock.Text = eventModel.Title;
            StartTimeTextBlock.Text = eventModel.Start.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            EndDateTextBlock.Text = eventModel.Start == eventModel.End ? "Todo el día" : "Finaliza " + eventModel.End.ToString("dddd dd", System.Globalization.CultureInfo.GetCultureInfo("es")) + eventModel.End.ToString(", h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            PlaceTextBlock.Text = eventModel.Place;
            ThematicTextBlock.Text = eventModel.Thematic;
            OrganizarTextBlock.Text = eventModel.Organizer;
            AboutTextBlock.Text = eventModel.About;

            if (eventModel.WithIns.Count <= 0)
            {
                WithInsWrapTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                WithInsTextBlock.Text = string.Join(", ", eventModel.WithIns.Select(item => item.FullName));
            }

            using (var data = new System.IO.MemoryStream(eventModel.PosterImage.CachedData))
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
        }

        public EventHandler PresentationShowInEventHandler { get; set; }
        public EventHandler PresentationEndedEventHandler { get; set; }

        public TimeSpan RequiredExternalDelayTime => TimeSpan.FromSeconds(animateShowIn ? CachedDataProvider.GetSingleInstance().Configuration.EventDetailDisplayTime + 2 : CachedDataProvider.GetSingleInstance().Configuration.EventDetailDisplayTime);

        public bool RequireFullScreen => false;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WrapContainer.Width = ActualWidth;
            WrapContainer.Height = ActualHeight;

            BillboardSidebar.Width = ActualWidth / 6.5;
            BillboardSidebar.MaxWidth = BillboardSidebar.Width;
            BillboardSidebar.MinWidth = BillboardSidebar.Width;

            if (animateShowIn)
            {
                PerformShowInAnimation();
            }
            else
            {
                PresentationShowInEventHandler(this, new EventArgs());
            }
        }

        private void PerformShowInAnimation()
        {
            const string WHITE_OPAQUE_RECTANGLE_NAME = "WhiteOpaqueRectable";
            const string RED_OPAQUE_BACKGROUND_RECTANGLE_NAME = "RedOpaqueBackgroundRectable";
            const string RED_SOLID_RECTANGLE_NAME = "RedSolidRectable";
            const string RED_OPAQUE_FOREGROUND_RECTANGLE_NAME = "RedOpaqueForegroundRectable";

            var layerColor = Color.FromRgb(0, 24, 49);

            var whiteOpaqueRectangle = new Rectangle { Width = ActualWidth, Height = ActualHeight };

            whiteOpaqueRectangle.Fill = new SolidColorBrush(Colors.White);
            whiteOpaqueRectangle.Opacity = 0.5;

            Canvas.SetLeft(whiteOpaqueRectangle, ActualWidth);

            RegisterName(WHITE_OPAQUE_RECTANGLE_NAME, whiteOpaqueRectangle);

            BackgroundAnimationContainer.Children.Add(whiteOpaqueRectangle);

            var redOpaqueBackgroudRectangle = new Rectangle { Width = ActualWidth, Height = ActualHeight };

            redOpaqueBackgroudRectangle.Fill = new SolidColorBrush(layerColor);
            redOpaqueBackgroudRectangle.Opacity = 0.75;

            Canvas.SetLeft(redOpaqueBackgroudRectangle, ActualWidth);

            RegisterName(RED_OPAQUE_BACKGROUND_RECTANGLE_NAME, redOpaqueBackgroudRectangle);

            BackgroundAnimationContainer.Children.Add(redOpaqueBackgroudRectangle);

            var redOpaqueForegroudRectangle = new Rectangle { Width = ActualWidth * 0.75, Height = ActualHeight };

            redOpaqueForegroudRectangle.Fill = new SolidColorBrush(layerColor);
            redOpaqueForegroudRectangle.Opacity = 0.75;

            Canvas.SetLeft(redOpaqueForegroudRectangle, ActualWidth);

            RegisterName(RED_OPAQUE_FOREGROUND_RECTANGLE_NAME, redOpaqueForegroudRectangle);

            ForegroundAnimationContainer.Children.Add(redOpaqueForegroudRectangle);

            var redSolidRectangle = new Rectangle { Width = ActualWidth, Height = ActualHeight };

            redSolidRectangle.Fill = new SolidColorBrush(layerColor);

            Canvas.SetLeft(redSolidRectangle, ActualWidth);

            RegisterName(RED_SOLID_RECTANGLE_NAME, redSolidRectangle);

            ForegroundAnimationContainer.Children.Add(redSolidRectangle);

            Canvas.SetLeft(WrapContainer, ActualWidth);

            var storyboard = new Storyboard
            {
                BeginTime = TimeSpan.FromSeconds(2)
            };

            var whiteOpaqueRectangleAnimation = new DoubleAnimation(ActualWidth * 0.35, TimeSpan.FromSeconds(0.5))
            {
                EasingFunction = new QuarticEase { },
            };

            Storyboard.SetTargetName(whiteOpaqueRectangleAnimation, WHITE_OPAQUE_RECTANGLE_NAME);
            Storyboard.SetTargetProperty(whiteOpaqueRectangleAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(whiteOpaqueRectangleAnimation);

            var redOpaqueBackgroundRectangleAnimation = new DoubleAnimation(ActualWidth * 0.35, TimeSpan.FromSeconds(0.75))
            {
                BeginTime = TimeSpan.FromSeconds(0.15),
                EasingFunction = new QuarticEase { }
            };

            Storyboard.SetTargetName(redOpaqueBackgroundRectangleAnimation, RED_OPAQUE_BACKGROUND_RECTANGLE_NAME);
            Storyboard.SetTargetProperty(redOpaqueBackgroundRectangleAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(redOpaqueBackgroundRectangleAnimation);

            var redSolidRectangleAnimation = new DoubleAnimation(redSolidRectangle.Width * -1, TimeSpan.FromSeconds(2))
            {
                BeginTime = TimeSpan.FromSeconds(0.4),
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTargetName(redSolidRectangleAnimation, RED_SOLID_RECTANGLE_NAME);
            Storyboard.SetTargetProperty(redSolidRectangleAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(redSolidRectangleAnimation);

            var redOpaqueForegroudRectangleAnimation = new DoubleAnimation(redOpaqueForegroudRectangle.Width * -1, TimeSpan.FromSeconds(2.5))
            {
                BeginTime = TimeSpan.FromSeconds(0.45),
                EasingFunction = new CircleEase { }
            };

            Storyboard.SetTargetName(redOpaqueForegroudRectangleAnimation, RED_OPAQUE_FOREGROUND_RECTANGLE_NAME);
            Storyboard.SetTargetProperty(redOpaqueForegroudRectangleAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(redOpaqueForegroudRectangleAnimation);

            var contentAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(2))
            {
                BeginTime = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTargetName(contentAnimation, WrapContainer.Name);
            Storyboard.SetTargetProperty(contentAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(contentAnimation);

            storyboard.Completed += OnPresentationStarted;

            storyboard.Begin(this);
        }

        private void OnPresentationStarted(object sender, EventArgs e)
        {
            BackgroundAnimationContainer.Children.Clear();
            ForegroundAnimationContainer.Children.Clear();

            PresentationShowInEventHandler(this, e);
        }
    }
}
