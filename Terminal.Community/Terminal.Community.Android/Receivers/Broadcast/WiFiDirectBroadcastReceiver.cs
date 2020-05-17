namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Receivers.Broadcast
{
    public class WiFiDirectBroadcastReceiver : Android.Content.BroadcastReceiver
    {
        private Managers.WiFiDataShareManager wifiDataShareManager;
        private Android.App.Activity activity;

        public WiFiDirectBroadcastReceiver(Managers.WiFiDataShareManager wifiDataShareManager, Android.App.Activity activity)
        {
            this.wifiDataShareManager = wifiDataShareManager;
            this.activity = activity;
        }

        public override void OnReceive(Android.Content.Context context, Android.Content.Intent intent)
        {
            var wifiDirectManager = (Android.Net.Wifi.P2p.WifiP2pManager)activity.GetSystemService(Android.Content.Context.WifiP2pService);

            switch (intent.Action)
            {
                case Android.Net.Wifi.P2p.WifiP2pManager.WifiP2pStateChangedAction:
                    break;

                case Android.Net.Wifi.P2p.WifiP2pManager.WifiP2pPeersChangedAction:
                    wifiDirectManager.RequestPeers(wifiDataShareManager.WifiDirectChannel, new Listeners.WiFiDataShareP2pManagerPeerListListener(wifiDataShareManager));

                    break;

                case Android.Net.Wifi.P2p.WifiP2pManager.WifiP2pConnectionChangedAction:
                    var networkInfo = (Android.Net.NetworkInfo)intent.GetParcelableExtra(Android.Net.Wifi.P2p.WifiP2pManager.ExtraNetworkInfo);

                    if (!networkInfo.IsConnected) return;

                    wifiDirectManager.RequestConnectionInfo(wifiDataShareManager.WifiDirectChannel, new Listeners.WiFiDataShareP2pManagerConnectionInfoListener(wifiDataShareManager));

                    break;

                case Android.Net.Wifi.P2p.WifiP2pManager.WifiP2pThisDeviceChangedAction:
                    var device = (Android.Net.Wifi.P2p.WifiP2pDevice)intent.GetParcelableExtra(Android.Net.Wifi.P2p.WifiP2pManager.ExtraWifiP2pDevice);

                    wifiDataShareManager.DeviceName = device.DeviceName;

                    break;
            }
        }
    }
}