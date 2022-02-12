using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static BackgroundTaskDeferral AppServiceDeferral = null;
        public static AppServiceConnection Connection = null;
        public static event EventHandler AppService_Disconnected;
        public static event EventHandler<AppServiceTriggerDetails> AppService_Connected;
        public static bool IsForeground = false;

        private Windows.System.Display.DisplayRequest displayRequest;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            displayRequest = new Windows.System.Display.DisplayRequest();

            Windows.UI.ViewManagement.ApplicationView.PreferredLaunchWindowingMode = Windows.UI.ViewManagement.ApplicationViewWindowingMode.FullScreen;

            Suspending += App_Suspending;
            EnteredBackground += App_EnteredBackground;
            LeavingBackground += App_LeavingBackground;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                //var a = Windows.System.User.GetFromId(string.Empty);

                //var identity = System.Security.Principal.WindowsIdentity.GetCurrent();

                FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

                // TODO Remove when feature released
                Xamarin.Forms.Forms.SetFlags("Brush_Experimental", "Expander_Experimental");
                //Xamarin.Forms.Forms.SetFlags("Shapes_Experimental");

                var assembliesToInclude = new System.Collections.Generic.List<System.Reflection.Assembly>
                {
                    typeof(CarouselView.FormsPlugin.UWP.CarouselViewRenderer).Assembly,
                    typeof(ImageCircle.Forms.Plugin.UWP.ImageCircleRenderer).Assembly
                };

                Xamarin.Forms.Forms.Init(e, assembliesToInclude);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;

                if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
                {
                    await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                }
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(Pages.MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

            Window.Current.Closed += Window_Closed;

            displayRequest.RequestActive();
        }

        private async void Window_Closed(object sender, Windows.UI.Core.CoreWindowEventArgs e)
        {
            var supportInteropServiceRequest = new Windows.Foundation.Collections.ValueSet
            {
                { Common.Providers.ConstantProvider.COMMAND_REQUEST_VALUESET_KEY, Common.Providers.ConstantProvider.COMMAND_EXIT_ENVIRONMENT_REQUEST }
            };

            await Connection.SendMessageAsync(supportInteropServiceRequest);
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Handles connection requests to the app service
        /// </summary>
        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            base.OnBackgroundActivated(args);

            if (args.TaskInstance.TriggerDetails is AppServiceTriggerDetails details)
            {
                // only accept connections from callers in the same package
                if (details.CallerPackageFamilyName == Package.Current.Id.FamilyName)
                {
                    // connection established from the fulltrust process
                    AppServiceDeferral = args.TaskInstance.GetDeferral();
                    
                    args.TaskInstance.Canceled += BackgroundActivationTask_Canceled;

                    Connection = details.AppServiceConnection;

                    AppService_Connected?.Invoke(this, args.TaskInstance.TriggerDetails as AppServiceTriggerDetails);
                }
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void App_Suspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();

            try
            {
                displayRequest.RequestRelease();
            }
            catch { }
        }

        private void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            IsForeground = true;
        }

        private void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            IsForeground = false;
        }

        /// <summary>
        /// Task canceled here means the app service client is gone
        /// </summary>
        private void BackgroundActivationTask_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            AppServiceDeferral?.Complete();

            AppServiceDeferral = null;
            Connection = null;

            AppService_Disconnected?.Invoke(this, null);
        }
    }
}
