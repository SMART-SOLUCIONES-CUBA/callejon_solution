
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls
{
    /// <summary>
    /// Interaction logic for BillboardSlideshowControl.xaml
    /// </summary>
    public partial class BillboardSlideshowControl : UserControl, Presenters.Control.IPresentableControl
    {
        private Presenters.ContentSlideshowPresenter presenter;

        public BillboardSlideshowControl()
        {
            InitializeComponent();

            presenter = new Presenters.ContentSlideshowPresenter(PersistentSlideContainer, TransitionSlideContainer, AdvisesTextBlock, OnPresentationEnded);
        }

        public EventHandler PresentationShowInEventHandler { get; set; }
        public EventHandler PresentationEndedEventHandler { get; set; }

        public TimeSpan RequiredExternalDelayTime => TimeSpan.Zero;

        public bool RequireFullScreen => false;

        private void AdvisesTextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var targetWidth = AdvisesCanvas.ActualWidth > AdvisesTextBlock.ActualWidth ? AdvisesCanvas.ActualWidth : AdvisesTextBlock.ActualWidth;

            var animation = new DoubleAnimation
            {
                From = AdvisesTextBlock.ActualWidth * -1,
                To = AdvisesCanvas.ActualWidth,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(targetWidth / 50))
            };

            AdvisesTextBlock.BeginAnimation(Canvas.RightProperty, animation, HandoffBehavior.Compose);
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

            ContentContainer.Width = ActualWidth;
            ContentContainer.Height = ActualHeight;

            Canvas.SetLeft(ContentContainer, ActualWidth);

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

            contentAnimation.Completed += PresentationShowInEventHandler;

            Storyboard.SetTargetName(contentAnimation, ContentContainer.Name);
            Storyboard.SetTargetProperty(contentAnimation, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(contentAnimation);

            storyboard.Completed += OnPresentationStarted;

            storyboard.Begin(this);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PerformShowInAnimation();

            presenter.InitPresentation();

            presenter.FullScreenRequired += OnFullScreenRequired;
            presenter.FullScreenUnrequired += OnFullScreenUnrequired;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs eventArgs)
        {
            presenter.FullScreenRequired -= OnFullScreenRequired;
            presenter.FullScreenUnrequired -= OnFullScreenUnrequired;
        }

        private void OnFullScreenRequired(object sender, EventArgs eventArgs)
        {
            MarqueeWrapContainer.Visibility = Visibility.Collapsed;
        }

        private void OnFullScreenUnrequired(object sender, EventArgs eventArgs)
        {
            MarqueeWrapContainer.Visibility = Visibility.Visible;
        }

        private void OnPresentationStarted(object sender, EventArgs e)
        {
            BackgroundAnimationContainer.Children.Clear();
            ForegroundAnimationContainer.Children.Clear();
        }

        private void OnPresentationEnded(object sender, EventArgs e)
        {
            PresentationEndedEventHandler(sender, e);
        }
    }
}
