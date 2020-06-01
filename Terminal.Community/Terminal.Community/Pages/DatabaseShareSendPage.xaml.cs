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

                var userProfile = Managers.ProfileManager.Instance.GetCurrentProfile();

                var hostFriendDataModel = new Models.Data.FriendDataModel
                {
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    IsMale = userProfile.IsMale,
                    Terminal = new Models.Data.TerminalDataModel
                    {
                        Manufacturer = Xamarin.Essentials.DeviceInfo.Manufacturer,
                        DeviceType = Xamarin.Essentials.DeviceInfo.DeviceType,
                        Idiom = Xamarin.Essentials.DeviceInfo.Idiom,
                        Model = Xamarin.Essentials.DeviceInfo.Model,
                        Name = Xamarin.Essentials.DeviceInfo.Name,
                        Platform = Xamarin.Essentials.DeviceInfo.Platform,
                        Version = Xamarin.Essentials.DeviceInfo.Version,
                        VersionString = Xamarin.Essentials.DeviceInfo.VersionString
                    }
                };

                //Net.PresenceSocketServer.Instance.Run(hostFriendDataModel, cancellationTokenSource.Token, OnPresenceDataReceiveCallback);

                await BroadcastServerPresenceAsync(hostFriendDataModel, cancellationTokenSource.Token);
            });
        }

        private void OnShutdownRequestCallback(bool success)
        {
            if (!IsEnabled) return;
        }

        private void OnPresenceDataReceiveCallback(Models.Data.PresenceDataModel model)
        {
            (BindingContext as Models.Page.DatabaseShareSendPageModel).Presences.Add(model);
        }

        public async System.Threading.Tasks.Task BroadcastServerPresenceAsync(Models.Data.FriendDataModel hostFriendDataModel, System.Threading.CancellationToken cancellationToken)
        {
            var bradcastIPAddress = Xamarin.Forms.DependencyService.Get<Providers.Facade.INetworkInformationProviderFacade>().GetBreadcastIPAddress();

            var broadcastEndPoint = new System.Net.IPEndPoint(bradcastIPAddress, Providers.ConstantsProvider.SOCKET_BONJOUR_BROADCAST_SERVER_PORT);

            var broadcaster = new System.Net.Sockets.Socket(bradcastIPAddress.AddressFamily, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp);

            broadcaster.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.Broadcast, true);

            while (!cancellationToken.IsCancellationRequested)
            {
                var broadcastMessageContent = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}{1}", Newtonsoft.Json.JsonConvert.SerializeObject(hostFriendDataModel), Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK));

                broadcaster.SendTo(broadcastMessageContent, broadcastEndPoint);

                await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(5));
            }

            broadcaster.Close();
        }
    }
}