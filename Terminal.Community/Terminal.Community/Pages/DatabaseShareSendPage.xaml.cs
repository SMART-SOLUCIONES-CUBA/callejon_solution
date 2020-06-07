using System.Linq;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DatabaseShareSendPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private System.Threading.CancellationTokenSource cancellationTokenSource;

        public DatabaseShareSendPage()
        {
            InitializeComponent();

            BindingContext = new Models.Page.DatabaseShareSendPageModel();
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
            //stopwatch.Start();

            (BindingContext as Models.Page.DatabaseShareSendPageModel).IsBusy = true;

            //Xamarin.Forms.Device.StartTimer(System.TimeSpan.FromMilliseconds(33), () =>
            //{
            //    wavePropagationReference = (float)(stopwatch.Elapsed.TotalMilliseconds % CYCLE_TIME_MILLISECOUNDS / CYCLE_TIME_MILLISECOUNDS);

            //    CanvasView.InvalidateSurface();

            //    var @continue = (BindingContext as Models.Page.DatabaseShareRecivePageModel).IsBusy;

            //    if (!@continue)
            //    {
            //        stopwatch.Stop();
            //    }

            //    return @continue;
            //});
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

            if (context.Message.IndexOf(Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK) > -1)
            {
                var jsonData = context.Message.Substring(0, context.Message.Length - Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK.Length);

                var senderFriend = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Data.FriendDataModel>(jsonData);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var pageContext = BindingContext as Models.Page.DatabaseShareSendPageModel;

                    if (!pageContext.Presences.Any(item => item.Friend.Id == senderFriend.Id))
                    {
                        pageContext.Presences.Add(new Models.Data.PresenceDataModel
                        {
                            At = System.DateTime.Now,
                            Friend = senderFriend
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