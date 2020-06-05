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

            IsEnabled = false;

            BindingContext = new Models.Page.DatabaseShareSendPageModel
            {
                IsBusy = true,
                BusyProgressMessage = "Configurando",
                BusyProgressMessageDetail = "Preparando punto de acceso WiFi para tus amigos ..."
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!IsEnabled)
            {
                IsEnabled = true;

                Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().RegisterCallbackActions(OnCheckPermissionsCallback, OnStartupRequestCallback, OnShutdownRequestCallback);

                Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().CheckPermissions();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            OnClosing();

            return true;
        }

        private void CloseButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            OnClosing();
        }

        private void OnClosing()
        {
            DisplayAlert("Confirmar", "Está seguro que desea terminar el proceso Compartir con Amigos?", "Terminar", "Cancelar").ContinueWith(task =>
            {
                if (!task.Result) return;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    var pageModel = BindingContext as Models.Page.DatabaseShareSendPageModel;

                    pageModel.IsBusy = true;
                    pageModel.BusyProgressMessage = "Terminado";
                    pageModel.BusyProgressMessageDetail = "Apagando punto de acceso WiFi ...";

                    IsEnabled = false;

                    Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().RequestShutdown();

                    cancellationTokenSource.Cancel();

                    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(2));

                    await this.NavigationPopModalPageAsync();
                });                
            });
        }

        private void OnCheckPermissionsCallback(bool success)
        {
            if (success)
            {
                Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().RequestStartup();
            }
            else
            {
                DisplayAlert("Aviso", "Permisos requeridos", "Aceptar");

                this.NavigationPopModalPageAsync();
            }
        }

        private void OnStartupRequestCallback(Models.View.WiFiHostpotDetailViewModel model)
        {
            System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(2)).ContinueWith(async task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var pageModel = BindingContext as Models.Page.DatabaseShareSendPageModel;

                    pageModel.WiFiHostpotDetail = model;
                    pageModel.IsBusy = false;
                });

                await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(3));

                cancellationTokenSource = new System.Threading.CancellationTokenSource();

                var hostFriendDataModel = Factories.FriendDataModelFactory.CreateSelf();

                var networkInformation = Providers.NetworkInformationProvider.GetNetworkInformation();

                System.Threading.Tasks.Parallel.Invoke(() =>
                {
                    try
                    {
                        ListenForReceiverPresenceReport(networkInformation);
                    }
                    catch
                    {
                        // TODO Handle throwed exception!!
                        cancellationTokenSource.Cancel();
                    }
                }, async () =>
                {
                    await BroadcastSenderPresenceAsync(hostFriendDataModel, networkInformation, cancellationTokenSource.Token).ContinueWith(broadcastSenderPresenceTask =>
                    {
                        if (broadcastSenderPresenceTask.Exception != null) return;

                        // TODO Handle throwed exception!!
                        cancellationTokenSource.Cancel();
                    });
                });
            });
        }

        private void OnShutdownRequestCallback(bool success)
        {
            if (!IsEnabled) return;
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

            var receiverPresenceReadingContext = new ReceiverPresenceReadingContext(listener.EndAccept(result));

            receiverPresenceReadingContext.Socket.BeginReceive(receiverPresenceReadingContext.Buffer, 0, Providers.ConstantsProvider.SOCKET_BUFFER_LENGHT, System.Net.Sockets.SocketFlags.None, new System.AsyncCallback(OnReceiverPresenceReadCallback), receiverPresenceReadingContext);
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