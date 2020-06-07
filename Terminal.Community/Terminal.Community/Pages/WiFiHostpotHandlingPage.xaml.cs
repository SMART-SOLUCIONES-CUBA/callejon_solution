using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class WiFiHostpotHandlingPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private System.Action<Models.View.WiFiHostpotDetailViewModel> handlingCallbackAction;

        public WiFiHostpotHandlingPage(Models.View.WiFiHostpotDetailViewModel wiFiHostpotDetail, System.Action<Models.View.WiFiHostpotDetailViewModel> handlingCallbackAction)
        {
            InitializeComponent();

            this.handlingCallbackAction = handlingCallbackAction;

            BindingContext = new Models.Page.WiFiHostpotHandlingPageModel
            {
                WiFiHostpotDetail = wiFiHostpotDetail
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var pageContext = BindingContext as Models.Page.WiFiHostpotHandlingPageModel;

            pageContext.BusyProgressMessage = pageContext.WiFiHostpotDetail == null ?
                "Comprobando permisos requeridos ..." :
                string.Format("Apagando punto de acceso WiFi '{0}' ...", pageContext.WiFiHostpotDetail.SSID);

            Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().RegisterCallbackActions(OnCheckPermissionsCallback, OnStartupRequestCallback, OnShutdownRequestCallback);

            if (pageContext.WiFiHostpotDetail == null)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().CheckPermissions);
            }
            else
            {
                System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(2)).ContinueWith(task =>
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().RequestShutdown);
                });
            }
        }

        private void OnCheckPermissionsCallback(bool success)
        {
            if (success)
            {
                (BindingContext as Models.Page.WiFiHostpotHandlingPageModel).BusyProgressMessage = "Preparando punto de acceso WiFi para tu grupo amigos ...";

                System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(2)).ContinueWith(task =>
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().RequestStartup);
                });
            }
            else
            {
                Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().ReleaseCallbackActions();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    handlingCallbackAction.Invoke(null);

                    handlingCallbackAction = null;

                    this.NavigationPopModalPageAsync();
                });
            }
        }

        private void OnStartupRequestCallback(Models.View.WiFiHostpotDetailViewModel model)
        {
            Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().ReleaseCallbackActions();

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                handlingCallbackAction.Invoke(model);

                handlingCallbackAction = null;

                this.NavigationPopModalPageAsync();
            });

            //System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(2)).ContinueWith(async task =>
            //{
            //    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            //    {
            //        var pageModel = BindingContext as Models.Page.DatabaseShareSendPageModel;

            //        pageModel.WiFiHostpotDetail = model;
            //        pageModel.IsBusy = false;
            //    });

            //    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(3));

            //    cancellationTokenSource = new System.Threading.CancellationTokenSource();

            //    var hostFriendDataModel = Factories.FriendDataModelFactory.CreateSelf();

            //    var networkInformation = Providers.NetworkInformationProvider.GetNetworkInformation();

            //    System.Threading.Tasks.Parallel.Invoke(() =>
            //    {
            //        try
            //        {
            //            ListenForReceiverPresenceReport(networkInformation);
            //        }
            //        catch
            //        {
            //            // TODO Handle throwed exception!!
            //            cancellationTokenSource.Cancel();
            //        }
            //    }, async () =>
            //    {
            //        await BroadcastSenderPresenceAsync(hostFriendDataModel, networkInformation, cancellationTokenSource.Token).ContinueWith(broadcastSenderPresenceTask =>
            //        {
            //            if (broadcastSenderPresenceTask.Exception != null) return;

            //            // TODO Handle throwed exception!!
            //            cancellationTokenSource.Cancel();
            //        });
            //    });
            //});
        }

        private void OnShutdownRequestCallback(bool success)
        {
            Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>().ReleaseCallbackActions();

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                handlingCallbackAction.Invoke(success ? null : (BindingContext as Models.Page.WiFiHostpotHandlingPageModel).WiFiHostpotDetail);

                handlingCallbackAction = null;

                this.NavigationPopModalPageAsync();
            });
        }
    }
}
