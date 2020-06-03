using System.Linq;
using System.Net.Sockets;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using SkiaSharp.Views.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DatabaseShareReceivePage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private const int CYCLE_TIME_MILLISECOUNDS = 1000;

        private SkiaSharp.SKColor wavePropagationColor;

        private float wavePropagationReference;

        private System.Collections.Generic.Queue<GridCoordinate> availableGridCoordenates;

        private System.Diagnostics.Stopwatch stopwatch;

        private System.Threading.CancellationTokenSource cancellationTokenSource;

        public DatabaseShareReceivePage()
        {
            InitializeComponent();

            availableGridCoordenates = new System.Collections.Generic.Queue<GridCoordinate>();

            var secoundaryBrandColor = (Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["SecoundaryBrandColor"];

            wavePropagationColor = secoundaryBrandColor.WithLuminosity(0.85).ToSKColor();

            stopwatch = new System.Diagnostics.Stopwatch();

            BindingContext = new Models.Page.DatabaseShareRecivePageModel();

            cancellationTokenSource = new System.Threading.CancellationTokenSource();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StartSenderSearchIfValidNetworkAccess(Xamarin.Essentials.Connectivity.NetworkAccess);

            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void SKCanvasView_PaintSurface(object sender,  SKPaintSurfaceEventArgs args)
        {
            args.Surface.Canvas.Clear();

            var center = new SkiaSharp.SKPoint(args.Info.Width / 2, args.Info.Height / 2);

            float baseRadius = System.Math.Min(args.Info.Width, args.Info.Height) / 12;

            var colorRed = wavePropagationColor.Red;
            var colorGreen = wavePropagationColor.Green;
            var colorBlue = wavePropagationColor.Blue;

            for (int circle = 0; circle < 5; circle++)
            {
                float radius = baseRadius * (circle + wavePropagationReference);

                var paint = new SkiaSharp.SKPaint
                {
                    Style = SkiaSharp.SKPaintStyle.Stroke
                };

                paint.StrokeWidth = baseRadius / 2 * (circle == 0 ? wavePropagationReference : 1);

                paint.Color = new SkiaSharp.SKColor(colorRed, colorGreen, colorBlue, (byte)(255 * (circle == 4 ? (1 - wavePropagationReference) : 1)));

                args.Surface.Canvas.DrawCircle(center.X, center.Y, radius, paint);
            }
        }

        private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs args)
        {
            cancellationTokenSource?.Cancel();

            cancellationTokenSource = null;

            StartSenderSearchIfValidNetworkAccess(args.NetworkAccess);
        }

        private void QrCodeScanToolbarItem_Clicked(object sender, System.EventArgs args)
        {
            StopSenderSearch();

            this.NavigationPushPageAsync(new QRCodeScanPage(OnQrCodeScanSuccess));
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
                        wifiAccessManager.RegisterCallbackActions(OnWiFiAccessCallback);

                        wifiAccessManager.Connect(ssid, password);
                    }
                    else
                    {
                        DisplayAlert("Confirmar", string.Format("Está seguro que desea conectarse a la red WiFi '{0}'", ssid), "Aceptar", "Cancelar").ContinueWith(task =>
                        {
                            wifiAccessManager.RegisterCallbackActions(OnWiFiAccessCallback);

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

        private void OnWiFiAccessCallback(int errorCode)
        {
            Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiAccessManagerFacade>().ReleaseCallbackActions();

            if (errorCode == 13)
            {
                DisplayAlert("Aviso", "Este dispositivo ya esta conectado a la red WiFi especificada.", "Aceptar");

                StartSenderSearchIfValidNetworkAccess(Xamarin.Essentials.Connectivity.NetworkAccess);
            }
            else if (errorCode > 0)
            {
                DisplayAlert("Aviso", "No fue posible conectarse a la red WiFi especificada.", "Aceptar");
            }
        }

        private async System.Threading.Tasks.Task ListenForSenderBroadcastAsync(System.Threading.CancellationToken cancellationToken)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(5));

            var broadcastingEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, Providers.ConstantsProvider.SOCKET_BONJOUR_BROADCAST_SERVER_PORT);

            var referenceableBroadcastingEndPoint = (System.Net.EndPoint)broadcastingEndPoint;

            var broadcastingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            broadcastingSocket.Bind(broadcastingEndPoint);

            while (!cancellationToken.IsCancellationRequested)
            {
                GridCoordinate targetGridCoordenate = null;

                if (availableGridCoordenates.Count > 0)
                {
                    targetGridCoordenate = availableGridCoordenates.Dequeue();
                }

                if (targetGridCoordenate == null)
                {
                    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(60));

                    continue;
                }

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

                        if (!pageContext.Candidates.Any(item => item.Friend.Id == senderFriend.Id))
                        {
                            pageContext.Candidates.Add(new Models.View.DataShareSenderViewModel
                            {
                                Friend = senderFriend,
                                GridColumn = targetGridCoordenate.Column,
                                GridRow = targetGridCoordenate.Row
                            });
                        }
                    });                    

                    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(5));
                }
            }

            broadcastingSocket.Close();
        }

        private void OnListenForSenderBroadcastCallback(System.Threading.Tasks.Task task)
        {
            if (task.Exception == null) return;

            cancellationTokenSource = null;

            // TODO Reempazar el mensaje de error!
            DisplayAlert("Aviso", "Error buscando amigos que desean compartir sus datos.", "Reintentar", "Cancelar").ContinueWith(alertTask =>
            {
                if (alertTask.Result)
                {
                    StartSenderSearchIfValidNetworkAccess(Xamarin.Essentials.Connectivity.NetworkAccess);
                }
                else
                {
                    this.NavigationPopPageAsync();
                }
            });
        }

        private void StartSenderSearchIfValidNetworkAccess(Xamarin.Essentials.NetworkAccess networkAccess)
        {
            var pageContext = BindingContext as Models.Page.DatabaseShareRecivePageModel;

            if (networkAccess == Xamarin.Essentials.NetworkAccess.None)
            {
                cancellationTokenSource?.Cancel();

                cancellationTokenSource = null;

                pageContext.IsBusy = false;
                pageContext.Sender = null;
                pageContext.Candidates.Clear();

                return;
            }

            var candiateGridCoordinates = new System.Collections.Generic.List<GridCoordinate>();

            for (int column = 0; column < 5; column++)
            {
                for (int row = 0; row < 5; row++)
                {
                    if (column == 2 && row == 2) continue;
                    if (column == 0 && row == 0) continue;
                    if (column == 0 && row == 4) continue;
                    if (column == 4 && row == 0) continue;
                    if (column == 4 && row == 4) continue;

                    candiateGridCoordinates.Add(new GridCoordinate { Column = column, Row = row });
                }
            }

            availableGridCoordenates.Clear();

            while (candiateGridCoordinates.Count > 0)
            {
                var randomizer = new System.Random();

                var targetIndex = randomizer.Next(0, candiateGridCoordinates.Count);

                var target = candiateGridCoordinates.ElementAt(targetIndex);

                availableGridCoordenates.Enqueue(target);

                candiateGridCoordinates.RemoveAt(targetIndex);
            }

            EnableReciveDataAnimation();

            cancellationTokenSource = new System.Threading.CancellationTokenSource();

            System.Threading.Tasks.Task.Run(() =>
            {
                ListenForSenderBroadcastAsync(cancellationTokenSource.Token).ContinueWith(OnListenForSenderBroadcastCallback);
            });
        }

        private void StopSenderSearch()
        {
            cancellationTokenSource?.Cancel();

            (BindingContext as Models.Page.DatabaseShareRecivePageModel).IsBusy = false;

            cancellationTokenSource = null;

            Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        private void EnableReciveDataAnimation()
        {
            stopwatch.Start();

            (BindingContext as Models.Page.DatabaseShareRecivePageModel).IsBusy = true;

            Xamarin.Forms.Device.StartTimer(System.TimeSpan.FromMilliseconds(33), () =>
            {
                wavePropagationReference = (float)(stopwatch.Elapsed.TotalMilliseconds % CYCLE_TIME_MILLISECOUNDS / CYCLE_TIME_MILLISECOUNDS);

                CanvasView.InvalidateSurface();

                var @continue = (BindingContext as Models.Page.DatabaseShareRecivePageModel).IsBusy;

                if (!@continue)
                {
                    stopwatch.Stop();
                }

                return @continue;
            });
        }

        //private void DisableReviveDataAnimation()
        //{
        //    (BindingContext as Models.Page.DatabaseShareRecivePageModel).IsBusy = false;
        //}

        private class GridCoordinate
        {
            internal int Column { get; set; }

            internal int Row { get; set; }
        }
    }
}