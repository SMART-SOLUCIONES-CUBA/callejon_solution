using UIKit;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Foundation.Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, Foundation.NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();

            LeoJHarris.FormsPlugin.iOS.EnhancedEntryRenderer.Init();

            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();

            Stormlion.ImageCropper.iOS.Platform.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            Xamarin.Forms.Forms.Init();
            
            LoadApplication(new App());

            var result = base.FinishedLaunching(app, options); 
            
            UIApplication.SharedApplication.KeyWindow.TintColor = UIColor.FromRGB(0, 24, 50); 
            
            return result;
        }
    }
}
