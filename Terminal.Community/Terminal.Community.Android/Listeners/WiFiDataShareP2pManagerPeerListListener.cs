using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Listeners
{
    public class WiFiDataShareP2pManagerPeerListListener : Java.Lang.Object, Android.Net.Wifi.P2p.WifiP2pManager.IPeerListListener
    {
        private Managers.WiFiDataShareManager wifiDataShareManager;

        public WiFiDataShareP2pManagerPeerListListener(Managers.WiFiDataShareManager wifiDataShareManager)
        {
            this.wifiDataShareManager = wifiDataShareManager;
        }

        public void OnPeersAvailable(Android.Net.Wifi.P2p.WifiP2pDeviceList peers)
        {
            wifiDataShareManager.Presenter.PresentAvailableFriendDevices(peers.DeviceList.Select(device => new Models.View.FriendDeviceViewModel
            {
                Name = device.DeviceName,
                NetAddress = device.DeviceAddress,
                PrimaryDeviceType = device.PrimaryDeviceType,
                SecondaryDeviceType = device.SecondaryDeviceType
            }).ToArray());
        }
    }
}