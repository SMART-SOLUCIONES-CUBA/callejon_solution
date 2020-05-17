namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Listeners
{
    public class WiFiDataShareP2pManagerConnectionInfoListener : Java.Lang.Object, Android.Net.Wifi.P2p.WifiP2pManager.IConnectionInfoListener
    {
        private readonly Managers.WiFiDataShareManager wifiDataShareManager;

        public WiFiDataShareP2pManagerConnectionInfoListener(Managers.WiFiDataShareManager wifiDataShareManager)
        {
            this.wifiDataShareManager = wifiDataShareManager;
        }

        public void OnConnectionInfoAvailable(Android.Net.Wifi.P2p.WifiP2pInfo info)
        {
            if (!info.GroupFormed) return;

            if (!info.IsGroupOwner)
            {
                wifiDataShareManager.StartDataTransferServer();

                wifiDataShareManager.Presenter.PresentStepCompleted(Enumerations.WiFiDataShareStep.Connect, true);
            }
            else
            {
                wifiDataShareManager.StartDataTransferClient(info.GroupOwnerAddress);
            }
        }
    }
}