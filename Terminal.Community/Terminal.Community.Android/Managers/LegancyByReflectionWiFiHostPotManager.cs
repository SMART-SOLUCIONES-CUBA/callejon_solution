namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers
{
    public class LegancyByReflectionWiFiHostPotManager : Community.Managers.Facade.IWiFiHostPotManagerFacade
    {
        private HotSpotManager.WifiApManager wifiApManager;

        private System.Action<bool> checkPermissionsCallbackAction;

        private System.Action<Models.View.WiFiHostPotDetailViewModel> startupRequestCallbackAction;
        private System.Action<bool> shutdownRequestCallbackAction;

        public void RegisterCallbackActions(System.Action<bool> checkPermissionsCallbackAction, System.Action<Models.View.WiFiHostPotDetailViewModel> startupRequestCallbackAction, System.Action<bool> shutdownRequestCallbackAction)
        {
            this.checkPermissionsCallbackAction = checkPermissionsCallbackAction;

            this.startupRequestCallbackAction = startupRequestCallbackAction;
            this.shutdownRequestCallbackAction = shutdownRequestCallbackAction;
        }

        public void CheckPermissions()
        {
            checkPermissionsCallbackAction.Invoke(true);
        }

        public void RequestStartup()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                if (wifiApManager == null)
                {
                    wifiApManager = new HotSpotManager.WifiApManager(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                }

                wifiApManager.ShowWritePermissionSettings(false);

                var currentConfig = wifiApManager.GetWifiApConfiguration();

                var state = wifiApManager.GetWifiApState();

                var wifiHostPotConfig = new Android.Net.Wifi.WifiConfiguration
                {
                    Ssid = "MiCallejon"
                };

                if (wifiApManager.SetWifiApEnabled(wifiHostPotConfig, true))
                {
                    startupRequestCallbackAction.Invoke(new Models.View.WiFiHostPotDetailViewModel
                    {
                        SSID = wifiHostPotConfig.Ssid
                    });
                }
                else
                {
                    startupRequestCallbackAction.Invoke(null);
                }
            });
        }

        public void RequestShutdown()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                shutdownRequestCallbackAction(wifiApManager.SetWifiApEnabled(null, false));
            });
        }
    }
}