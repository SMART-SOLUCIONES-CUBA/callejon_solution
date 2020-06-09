using System.Linq;
using SkiaSharp.Views.Forms;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DatabaseShareSendPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private const int CYCLE_TIME_MILLISECOUNDS = 1000;

        private readonly SkiaSharp.SKColor wavePropagationColor;

        private float wavePropagationReference;

        private System.Collections.Generic.Queue<Models.GridCoordinateModel> availableGridCoordenates;

        private System.Diagnostics.Stopwatch stopwatch;

        private System.Threading.CancellationTokenSource cancellationTokenSource;

        public DatabaseShareSendPage()
        {
            InitializeComponent();

            availableGridCoordenates = new System.Collections.Generic.Queue<Models.GridCoordinateModel>();

            var secoundaryBrandColor = (Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["SecoundaryBrandColor"];

            wavePropagationColor = secoundaryBrandColor.WithLuminosity(0.85).ToSKColor();

            stopwatch = new System.Diagnostics.Stopwatch();

            BindingContext = new Models.Page.DatabaseShareSendPageModel
            {
                Profile = Managers.ProfileManager.Instance.GetCurrentProfile()
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StartSenderPostulationIfValidNetworkAccess(Xamarin.Essentials.Connectivity.NetworkAccess);

            if ((BindingContext as Models.Page.DatabaseShareSendPageModel).WiFiHostpotDetail != null) return;

            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        protected override bool OnBackButtonPressed()
        {
            HandleCloseRequest();

            return true;
        }

        private void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            args.Surface.Canvas.Clear();

            var center = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation == Xamarin.Essentials.DisplayOrientation.Portrait ?
                new SkiaSharp.SKPoint(args.Info.Width / 2, 0) :
                new SkiaSharp.SKPoint(0, args.Info.Height / 2);

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

        private void PresenceTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var model = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.DataShareReceiverPresenceViewModel;

            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new Popup.DataShareReceiverPresenceDetailPopupPage(model));
        }

        private void CloseButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            HandleCloseRequest();
        }

        private void CreateWiFiHostpotButton_Clicked(object sender, System.EventArgs args)
        {
            Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

            this.NavigationPushModalPageAsync(new WiFiHostpotHandlingPage((BindingContext as Models.Page.DatabaseShareSendPageModel).WiFiHostpotDetail, OnWiFiHostpotHandlingCallback));
        }

        private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs args)
        {
            cancellationTokenSource?.Cancel();

            cancellationTokenSource = null;

            StartSenderPostulationIfValidNetworkAccess(args.NetworkAccess);
        }

        private void HandleCloseRequest()
        {
            var pageContext = BindingContext as Models.Page.DatabaseShareSendPageModel;

            if (!pageContext.IsBusy)
            {
                this.NavigationPopModalPageAsync();

                return;
            }

            DisplayAlert("Confirmar", "Está seguro que desea terminar el proceso Compartir con Amigos?", "Terminar", "Cancelar").ContinueWith(task =>
            {
                if (!task.Result) return;

                cancellationTokenSource?.Cancel();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

                    pageContext.IsBusy = false;
                    pageContext.BusyProgressMessage = null;                    

                    pageContext.Presences.Clear();

                    if (pageContext.WiFiHostpotDetail == null)
                    {
                        this.NavigationPopModalPageAsync();

                        return;
                    }

                    IsEnabled = false;

                    this.NavigationPushModalPageAsync(new WiFiHostpotHandlingPage(pageContext.WiFiHostpotDetail, OnWiFiHostpotHandlingCallback));

                    pageContext.WiFiHostpotDetail = null;
                });                
            });
        }

        private void OnWiFiHostpotHandlingCallback(Models.View.WiFiHostpotDetailViewModel model)
        {
            var pageContext = BindingContext as Models.Page.DatabaseShareSendPageModel;

            if (model == null)
            {
                if (pageContext.WiFiHostpotDetail != null)
                {
                    // TODO Show toastlike alert for succesfull WiFiHostpot terminste!
                }
                else
                {
                    if (IsEnabled)
                    {
                        DisplayAlert("", "No fue posible apagar adecuadamente el punto de acceso WiFi creado para tus amigos.", "Aceptar");
                    }

                    this.NavigationPopModalPageAsync();

                    return;
                }
            }

            pageContext.WiFiHostpotDetail = model;
        }

        private void StartSenderPostulationIfValidNetworkAccess(Xamarin.Essentials.NetworkAccess networkAccess)
        {
            var pageContext = BindingContext as Models.Page.DatabaseShareSendPageModel;

            if (pageContext.WiFiHostpotDetail == null && networkAccess == Xamarin.Essentials.NetworkAccess.None)
            {
                cancellationTokenSource?.Cancel();

                cancellationTokenSource = null;

                pageContext.IsBusy = false;

                pageContext.WiFiHostpotDetail = null;

                pageContext.Presences.Clear();

                return;
            }

            cancellationTokenSource = new System.Threading.CancellationTokenSource();

            var hostFriendDataModel = Factories.FriendDataModelFactory.CreateSelf();

            var networkInformation = Providers.NetworkInformationProvider.GetNetworkInformation();

            var candiateGridCoordinates = new System.Collections.Generic.List<Models.GridCoordinateModel>();

            for (int column = 0; column < 5; column++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (column == 1 && row == 1) continue;
                    if (column == 0 && row == 0) continue;
                    if (column == 0 && row == 2) continue;
                    if (column == 2 && row == 0) continue;
                    if (column == 2 && row == 2) continue;

                    candiateGridCoordinates.Add(new Models.GridCoordinateModel { Column = column, Row = row });
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

            EnableSendDataAnimation();

            System.Threading.Tasks.Parallel.Invoke(() =>
            {
                try
                {
                    ListenForReceiverPresenceReport(networkInformation);
                }
                catch
                {
                    // TODO Handle throwed exception!!
                    cancellationTokenSource?.Cancel();
                }
            }, async () =>
            {
                await BroadcastSenderPresenceAsync(hostFriendDataModel, networkInformation, cancellationTokenSource.Token).ContinueWith(broadcastSenderPresenceTask =>
                {
                    if (broadcastSenderPresenceTask.Exception == null) return;

                    // TODO Handle throwed exception!!
                    cancellationTokenSource?.Cancel();
                });
            });
        }

        private void EnableSendDataAnimation()
        {
            stopwatch.Start();

            (BindingContext as Models.Page.DatabaseShareSendPageModel).IsBusy = true;

            Xamarin.Forms.Device.StartTimer(System.TimeSpan.FromMilliseconds(33), () =>
            {
                wavePropagationReference = (float)(stopwatch.Elapsed.TotalMilliseconds % CYCLE_TIME_MILLISECOUNDS / CYCLE_TIME_MILLISECOUNDS);

                CanvasView.InvalidateSurface();

                var @continue = (BindingContext as Models.Page.DatabaseShareSendPageModel).IsBusy;

                if (!@continue)
                {
                    stopwatch.Stop();
                }

                return @continue;
            });
        }

        #region Presensing Handling

        private void ListenForReceiverPresenceReport(Models.NetworkInformationModel networkInformation)
        {
            var listenEndPoint = new System.Net.IPEndPoint(networkInformation.UnicastIPAddress, Providers.ConstantsProvider.DATASHARE_RECEIVER_PRESENCE_UNICAST_SOCKET_PORT);

            var listener = new System.Net.Sockets.Socket(networkInformation.UnicastIPAddress.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

            listener.Bind(listenEndPoint);
            listener.Listen(100);

            listener.BeginAccept(new System.AsyncCallback(OnReceiverPresenceReportListenerAcceptCallback), listener);
        }

        private void OnReceiverPresenceReportListenerAcceptCallback(System.IAsyncResult result)
        {
            var listener = (System.Net.Sockets.Socket) result.AsyncState;

            if (!cancellationTokenSource.IsCancellationRequested)
            {
                listener.BeginAccept(new System.AsyncCallback(OnReceiverPresenceReportListenerAcceptCallback), listener);
            };

            try
            {
                var receiverPresenceReadingContext = new ReceiverPresenceReadingContext(listener.EndAccept(result));

                receiverPresenceReadingContext.Socket.BeginReceive(receiverPresenceReadingContext.Buffer, 0, Providers.ConstantsProvider.SOCKET_BUFFER_LENGHT, System.Net.Sockets.SocketFlags.None, new System.AsyncCallback(OnReceiverPresenceReadCallback), receiverPresenceReadingContext);
            }
            catch
            {
                if (!cancellationTokenSource.IsCancellationRequested)
                {
                    // TODO Handle workflow state!
                }
            }
        }

        private void OnReceiverPresenceReadCallback(System.IAsyncResult result)
        {
            var context = (ReceiverPresenceReadingContext) result.AsyncState;

            var readLength = context.Socket.EndReceive(result);

            if (readLength <= 0 || cancellationTokenSource.IsCancellationRequested) return;

            context.Message += System.Text.Encoding.UTF8.GetString(context.Buffer, 0, readLength);

            if (availableGridCoordenates.Count > 0 && context.Message.IndexOf(Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK) > -1)
            {
                var jsonData = context.Message.Substring(0, context.Message.Length - Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK.Length);

                var senderFriend = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Data.FriendDataModel>(jsonData);

                var targetGridCoordenate = availableGridCoordenates.Dequeue();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var pageContext = BindingContext as Models.Page.DatabaseShareSendPageModel;

                    if (!pageContext.Presences.Any(item => item.Friend.Id == senderFriend.Id))
                    {
                        pageContext.Presences.Add(new Models.View.DataShareReceiverPresenceViewModel
                        {
                            At = System.DateTime.Now,
                            Friend = senderFriend,
                            GridColumn = targetGridCoordenate.Column,
                            GridRow = targetGridCoordenate.Row
                        });
                    }
                });
            }
            else
            {
                if (!cancellationTokenSource.IsCancellationRequested)
                {
                    context.Socket.BeginReceive(context.Buffer, 0, Providers.ConstantsProvider.SOCKET_BUFFER_LENGHT, System.Net.Sockets.SocketFlags.None, new System.AsyncCallback(OnReceiverPresenceReadCallback), context);
                };
            }
        }

        private async System.Threading.Tasks.Task BroadcastSenderPresenceAsync(Models.Data.FriendDataModel hostFriendDataModel, Models.NetworkInformationModel networkInformation, System.Threading.CancellationToken cancellationToken)
        {
            var broadcastEndPoint = new System.Net.IPEndPoint(networkInformation.BroadcastIPAddress, Providers.ConstantsProvider.DATASHARE_SENDER_PRESENCE_BROADCAST_SOCKET_PORT);

            var broadcaster = new System.Net.Sockets.Socket(networkInformation.BroadcastIPAddress.AddressFamily, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp);

            broadcaster.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.Broadcast, true);

            while (!cancellationToken.IsCancellationRequested)
            {
                var broadcastMessageContent = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}{1}", Newtonsoft.Json.JsonConvert.SerializeObject(hostFriendDataModel), Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK));

                broadcaster.SendTo(broadcastMessageContent, broadcastEndPoint);

                await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(5));
            }

            broadcaster.Close();
        }

        private class ReceiverPresenceReadingContext
        {
            internal ReceiverPresenceReadingContext(System.Net.Sockets.Socket socket)
            {
                Buffer = new byte[Providers.ConstantsProvider.SOCKET_BUFFER_LENGHT];

                Socket = socket;
            }

            internal System.Net.Sockets.Socket Socket { get; private set; }

            internal byte[] Buffer { get; private set; }

            internal string Message { get; set; }
        }

        #endregion
    }
}