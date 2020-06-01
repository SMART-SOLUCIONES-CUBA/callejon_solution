using System.Linq;
using System.Net.Sockets;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DatabaseShareReceivePage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private System.Threading.CancellationTokenSource cancellationTokenSource;

        public DatabaseShareReceivePage()
        {
            InitializeComponent();

            BindingContext = new Models.Page.DatabaseShareRecivePageModel();

            cancellationTokenSource = new System.Threading.CancellationTokenSource();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs args)
        {
            if (!args.ConnectionProfiles.Contains(Xamarin.Essentials.ConnectionProfile.WiFi)) return;

            Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

            System.Threading.Tasks.Task.Run(async () =>
            {
                await ListenForSenderBroadcastAsync(cancellationTokenSource.Token);
            }).ContinueWith(task =>
            {
            });
        }

        private void QrCodeScanToolbarItem_Clicked(object sender, System.EventArgs args)
        {
            this.NavigationPushPageAsync(new QRCodeScanPage(OnQrCodeScanSuccess));

            Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        private async System.Threading.Tasks.Task ListenForSenderBroadcastAsync(System.Threading.CancellationToken cancellationToken)
        {
            var broadcastingEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, Providers.ConstantsProvider.SOCKET_BONJOUR_BROADCAST_SERVER_PORT);

            var referenceableBroadcastingEndPoint = (System.Net.EndPoint)broadcastingEndPoint;

            var broadcastingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            broadcastingSocket.Bind(broadcastingEndPoint);

            while (!cancellationToken.IsCancellationRequested)
            {
                string broadcastMessage = null;

                var broadcastContent = new byte[1024];

                int broadcastContentLength = broadcastingSocket.ReceiveFrom(broadcastContent, ref referenceableBroadcastingEndPoint);

                broadcastMessage += System.Text.Encoding.UTF8.GetString(broadcastContent, 0, broadcastContentLength);

                int endOfMessageMarkIndex = broadcastMessage.IndexOf(Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK);

                if (endOfMessageMarkIndex > -1)
                {
                    var jsonData = broadcastMessage.Substring(0, broadcastMessage.Length - Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK.Length);

                    var senderFriend = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Data.FriendDataModel>(jsonData);

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        var pageContext = BindingContext as Models.Page.DatabaseShareRecivePageModel;

                        if (pageContext.Friends.Any(item => item.Terminal.Name == senderFriend.Terminal.Name))
                        {
                            pageContext.Friends.Add(senderFriend);
                        }
                    });

                    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(5));
                }
            }

            broadcastingSocket.Close();
        }

        private void OnQrCodeScanSuccess(string result)
        {
            var match = System.Text.RegularExpressions.Regex.Match(result, @"^WIFI:T:WPA;S:(.+);P:(.+);H:false;?");

            if (match.Success)
            {
                var ssid = match.Groups[1].Value;
                var password = match.Groups[2].Value;

                var wifiAccessManager = Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiAccessManagerFacade>();

                if (wifiAccessManager.IsPlatformSupported)
                {
                    if (wifiAccessManager.HasBuilinConfirmation)
                    {
                        wifiAccessManager.RegisterCallbackActions(OnWiFiConnect);

                        wifiAccessManager.Connect(ssid, password);
                    }
                    else
                    {
                        DisplayAlert("Confirmar", string.Format("Está seguro que desea conectarse a la red WiFi '{0}'", ssid), "Aceptar", "Cancelar").ContinueWith(task =>
                        {
                            wifiAccessManager.RegisterCallbackActions(OnWiFiConnect);

                            wifiAccessManager.Connect(ssid, password);
                        });
                    }
                }
                else
                {
                    DisplayAlert("Aviso", "La versión del sistema operativo no permite establecer conexiones WiFi automáticamente. Debe conectarse manualmente.", "Aceptar");
                }
                
            }
            else
            {
                DisplayAlert("Aviso", "El contenido del código QR no es compatible.", "Aceptar");
            }
        }

        private void OnWiFiConnect(int errorCode)
        {
            if (errorCode == 13)
            {
                DisplayAlert("Aviso", "Este dispositivo ya esta conectado a la red WiFi especificada.", "Aceptar");
            }
            else if (errorCode > 0)
            {
                DisplayAlert("Aviso", "No fue posible conectarse a la red WiFi especificada.", "Aceptar");
            }
        }
    }
}