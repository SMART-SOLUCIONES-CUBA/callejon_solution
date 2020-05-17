[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers.WiFiDataShareManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers
{
    public class WiFiDataShareManager : Community.Managers.Facade.IWiFiDataShareManager
    {
        private readonly Android.Content.IntentFilter wifiDirectBroadcastIntentFilter;

        private Receivers.Broadcast.WiFiDirectBroadcastReceiver wifiDirectBroadcastReceiver;

        private Threads.DataTransferServerThread dataTransferServerThread;
        private Threads.DataTransferClientThread dataTransferClientThread;

        public WiFiDataShareManager()
        {
            wifiDirectBroadcastIntentFilter = new Android.Content.IntentFilter();

            wifiDirectBroadcastIntentFilter.AddAction(Android.Net.Wifi.P2p.WifiP2pManager.WifiP2pStateChangedAction);
            wifiDirectBroadcastIntentFilter.AddAction(Android.Net.Wifi.P2p.WifiP2pManager.WifiP2pPeersChangedAction);
            wifiDirectBroadcastIntentFilter.AddAction(Android.Net.Wifi.P2p.WifiP2pManager.WifiP2pConnectionChangedAction);
            wifiDirectBroadcastIntentFilter.AddAction(Android.Net.Wifi.P2p.WifiP2pManager.WifiP2pThisDeviceChangedAction);
        }

        public Presenters.Facade.IWiFiDataSharePresenterFacade Presenter { get; private set; }

        public Android.Net.Wifi.P2p.WifiP2pManager.Channel WifiDirectChannel { get; private set; }

        public string DeviceName { get; set; }

        public void HandleApplicationResume()
        {
            if (wifiDirectBroadcastReceiver == null) return;

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.RegisterReceiver(wifiDirectBroadcastReceiver, wifiDirectBroadcastIntentFilter);
        }

        public void HandleApplicationSleep()
        {
            if (wifiDirectBroadcastReceiver == null) return;

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.UnregisterReceiver(wifiDirectBroadcastReceiver);
        }

        public void StartSession(Presenters.Facade.IWiFiDataSharePresenterFacade presenter)
        {
            Presenter = presenter;

            var activity = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity;

            wifiDirectBroadcastReceiver = new Receivers.Broadcast.WiFiDirectBroadcastReceiver(this, activity);

            activity.RegisterReceiver(wifiDirectBroadcastReceiver, wifiDirectBroadcastIntentFilter);

            var wifiManager = (Android.Net.Wifi.WifiManager)activity.GetSystemService(Android.Content.Context.WifiService);

            if (!wifiManager.IsWifiEnabled)
            {
                wifiManager.SetWifiEnabled(true);
            }

            var wifiDirectManager = (Android.Net.Wifi.P2p.WifiP2pManager)activity.GetSystemService(Android.Content.Context.WifiP2pService);

            WifiDirectChannel = wifiDirectManager.Initialize(activity, activity.MainLooper, null);

            wifiDirectManager.DiscoverPeers(WifiDirectChannel, new Listeners.WiFiDataShareP2pManagerActionListener(this, Enumerations.WiFiDataShareStep.Discover));
        }

        public void EndSession()
        {
            if (dataTransferServerThread != null)
            {
                dataTransferServerThread.Interrupt();

                dataTransferServerThread = null;
            }

            Presenter = null;

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.UnregisterReceiver(wifiDirectBroadcastReceiver);

            wifiDirectBroadcastReceiver = null;

            WifiDirectChannel = null;

            var wifiManager = (Android.Net.Wifi.WifiManager)Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.GetSystemService(Android.Content.Context.WifiService);

            if (wifiManager.IsWifiEnabled)
            {
                wifiManager.SetWifiEnabled(false);
            }
        }

        public void Connect(Models.View.FriendDeviceViewModel model)
        {
            var connectionConfig = new Android.Net.Wifi.P2p.WifiP2pConfig
            {
                DeviceAddress = model.NetAddress
            };

            var wifiDirectManager = (Android.Net.Wifi.P2p.WifiP2pManager)Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.GetSystemService(Android.Content.Context.WifiP2pService);

            wifiDirectManager.Connect(WifiDirectChannel, connectionConfig, new Listeners.WiFiDataShareP2pManagerActionListener(this, Enumerations.WiFiDataShareStep.Connect));
        }

        public async System.Threading.Tasks.Task TansferStoredDataAsync()
        {
            while (dataTransferClientThread == null || dataTransferClientThread.Socket == null || !dataTransferClientThread.Socket.IsConnected)
            {
                await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));
            }

            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var files = Android.App.Application.Context.FilesDir.ListFiles();

            await dataTransferClientThread.Socket.OutputStream.WriteAsync(System.BitConverter.GetBytes(files.Length), 0, 32);

            foreach (var file in files)
            {
                var bytes = System.IO.File.ReadAllBytes(file.AbsolutePath);

                await dataTransferClientThread.Socket.OutputStream.WriteAsync(System.BitConverter.GetBytes(bytes.Length), 0, 64);

                await dataTransferClientThread.Socket.OutputStream.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        internal void StartDataTransferServer()
        {
            var dataTransferServerThread = new Threads.DataTransferServerThread();

            dataTransferServerThread.Start();
        }

        internal void StartDataTransferClient(Java.Net.InetAddress hostAddress)
        {
            dataTransferClientThread = new Threads.DataTransferClientThread(hostAddress);

            dataTransferClientThread.Start();
        }
    }
}