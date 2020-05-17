namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Listeners
{
    public class WiFiDataShareP2pManagerActionListener : Java.Lang.Object, Android.Net.Wifi.P2p.WifiP2pManager.IActionListener
    {
        private readonly Managers.WiFiDataShareManager wifiDataShareManager;
        private readonly Enumerations.WiFiDataShareStep step;

        public WiFiDataShareP2pManagerActionListener(Managers.WiFiDataShareManager wifiDataShareManager, Enumerations.WiFiDataShareStep step)
        {
            this.wifiDataShareManager = wifiDataShareManager;
            this.step = step;
        }

        public void OnFailure([Android.Runtime.GeneratedEnum] Android.Net.Wifi.P2p.WifiP2pFailureReason reason)
        {
            wifiDataShareManager.Presenter.PresentStepCompleted(step, false, reason.ToString());

            // TODO Remove block if not required!!
            //if (step == Enumerations.WiFiDataShareStep.Discover)
            //{
            //    wifiDataShareManager.EndSession();
            //}
        }

        public void OnSuccess()
        {
            wifiDataShareManager.Presenter.PresentStepCompleted(step, true);
        }
    }
}