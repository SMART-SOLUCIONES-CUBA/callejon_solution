[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers.ApplicationManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers
{
    public class ApplicationManager : Community.Managers.Facade.IApplicationManagerFacade
    {
        public void Close()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        public void LaunchSystemWiFiSetting()
        {
            Xamarin.Essentials.Platform.CurrentActivity.StartActivity(new Android.Content.Intent(Android.Net.Wifi.WifiManager.ActionPickWifiNetwork));
        }
    }
}