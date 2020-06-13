[assembly: Xamarin.Forms.ResolutionGroupName("XFToolKit")]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIKit.UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
