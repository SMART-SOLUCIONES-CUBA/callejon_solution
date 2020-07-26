[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Managers.ApplicationManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Managers
{
    public sealed class ApplicationManager : Community.Managers.Facade.IApplicationManagerFacade
    {
        public void Close()
        {
            System.Threading.Thread.CurrentThread.Abort();
        }

        public void LaunchSystemWiFiSetting()
        {
            var url = new Foundation.NSUrl("App-Prefs:root=WIFI");

            if (UIKit.UIApplication.SharedApplication.CanOpenUrl(url))
            {
                UIKit.UIApplication.SharedApplication.OpenUrl(url);
            }
        }
    }
}