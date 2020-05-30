namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers
{
    [Android.Support.Annotation.RequiresApi(Api = (int)Android.OS.BuildVersionCodes.O)]
    public sealed class OreoPlusWiFiHostpotManager : Community.Managers.Facade.IWiFiHostpotManagerFacade
    {
        private System.Action<bool> checkPermissionsCallbackAction;

        internal System.Action<Models.View.WiFiHostpotDetailViewModel> StartupRequestCallbackAction { get; private set; }
        internal System.Action<bool> ShutdownRequestCallbackAction { get; private set; }

        internal Android.Net.Wifi.WifiManager.LocalOnlyHotspotReservation Reservation { get; set; }

        public void RegisterCallbackActions(System.Action<bool> checkPermissionsCallbackAction, System.Action<Models.View.WiFiHostpotDetailViewModel> startupRequestCallbackAction, System.Action<bool> shutdownRequestCallbackAction)
        {
            this.checkPermissionsCallbackAction = checkPermissionsCallbackAction;

            StartupRequestCallbackAction = startupRequestCallbackAction;
            ShutdownRequestCallbackAction = shutdownRequestCallbackAction;
        }

        public void CheckPermissions()
        {
            if (Xamarin.Essentials.Platform.CurrentActivity.CheckSelfPermission(Android.Manifest.Permission.AccessFineLocation) == Android.Content.PM.Permission.Granted)
            {
                checkPermissionsCallbackAction.Invoke(true);
            }
            else
            {
                checkPermissionsCallbackAction.Invoke(false);

                Xamarin.Essentials.Platform.CurrentActivity.RequestPermissions(new string[] { Android.Manifest.Permission.AccessFineLocation }, 0);
            }
        }

        public void RequestStartup()
        {
            var wifiManager = (Android.Net.Wifi.WifiManager)Xamarin.Essentials.Platform.CurrentActivity.GetSystemService(Android.Content.Context.WifiService);

            wifiManager.StartLocalOnlyHotspot(new Callbacks.WifiManagerLocalOnlyHotspotCallback(this), null);
        }

        public void RequestShutdown()
        {
            Reservation.Close();
        }
    }
}