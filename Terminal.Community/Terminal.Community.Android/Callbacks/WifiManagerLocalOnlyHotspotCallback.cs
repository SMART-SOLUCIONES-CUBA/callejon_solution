namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Callbacks
{
    [Android.Support.Annotation.RequiresApi(Api = (int)Android.OS.BuildVersionCodes.O)]
    internal class WifiManagerLocalOnlyHotspotCallback : Android.Net.Wifi.WifiManager.LocalOnlyHotspotCallback
    {
        private readonly Managers.OreoPlusWiFiHostpotManager manager;

        public WifiManagerLocalOnlyHotspotCallback(Managers.OreoPlusWiFiHostpotManager manager)
        {
            this.manager = manager;
        }

        public override void OnStarted(Android.Net.Wifi.WifiManager.LocalOnlyHotspotReservation reservation)
        {
            base.OnStarted(reservation);

            manager.Reservation = reservation;

            manager.StartupRequestCallbackAction.Invoke(new Models.View.WiFiHostpotDetailViewModel
            {
                SSID = reservation.WifiConfiguration.Ssid,
                Password = reservation.WifiConfiguration.PreSharedKey
            });
        }

        public override void OnFailed([Android.Runtime.GeneratedEnum] Android.Net.Wifi.LocalOnlyHotspotCallbackErrorCode reason)
        {
            base.OnFailed(reason);

            manager.StartupRequestCallbackAction.Invoke(null);
        }

        public override void OnStopped()
        {
            // TODO Restore lines if WifiManagerLocalOnlyHotspotCallback::OnStopped other android 9 diff versions

            //manager.ShutdownRequestCallbackAction?.Invoke(true);

            //manager.Reservation?.Dispose();

            //manager.Reservation = null;
        }
    }
}