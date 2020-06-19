using System.Linq;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Extensions;
using SkiaSharp.Views.Forms;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DatabaseShareReceivePage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private const int CYCLE_TIME_MILLISECOUNDS = 1000;

        private static System.TimeSpan TIMER_TIME = System.TimeSpan.FromMilliseconds(33);
        private static System.TimeSpan RECEIVER_EXPIRE_TIME = System.TimeSpan.FromSeconds(5);

        private System.Action<byte[], System.DateTime> shareDataReceivedCallbackAction;

        private System.TimeSpan receiverPrecenceExpirationRefPoint;

        private readonly SkiaSharp.SKColor animationWavePropagationColor;

        private float animationWavePropagationRefPoint;

        private System.Collections.Generic.Queue<Models.GridCoordinateModel> availableGridCoordenates;

        private System.Diagnostics.Stopwatch stopwatch;

        private System.Threading.CancellationTokenSource cancellationTokenSource;

        public DatabaseShareReceivePage(System.Action<byte[], System.DateTime> shareDataReceivedCallbackAction)
        {
            InitializeComponent();

            this.shareDataReceivedCallbackAction = shareDataReceivedCallbackAction;

            availableGridCoordenates = new System.Collections.Generic.Queue<Models.GridCoordinateModel>();

            var secoundaryBrandColor = (Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["SecoundaryBrandColor"];

            animationWavePropagationColor = secoundaryBrandColor.WithLuminosity(0.85).ToSKColor();

            stopwatch = new System.Diagnostics.Stopwatch();

            BindingContext = new Models.Page.DatabaseShareRecivePageModel();

            cancellationTokenSource = new System.Threading.CancellationTokenSource();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Xamarin.Essentials.DeviceDisplay.KeepScreenOn = true;

            Xamarin.Forms.DependencyService.Get<Managers.Facade.IStatusBarApparenceManagerFacade>()?.RequireDarkTextApparence();

            StartSenderSearchIfValidNetworkAccess(Xamarin.Essentials.Connectivity.NetworkAccess);

            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void SKCanvasView_PaintSurface(object sender,  SKPaintSurfaceEventArgs args)
        {
            args.Surface.Canvas.Clear();

            var center = new SkiaSharp.SKPoint(args.Info.Width / 2, args.Info.Height / 2);

            float baseRadius = System.Math.Min(args.Info.Width, args.Info.Height) / 12;

            var colorRed = animationWavePropagationColor.Red;
            var colorGreen = animationWavePropagationColor.Green;
            var colorBlue = animationWavePropagationColor.Blue;

            var pageContext = BindingContext as Models.Page.DatabaseShareRecivePageModel;

            if (pageContext.IsReceivingBundle)
            {
                for (int circle = 5; circle >= 0; circle--)
                {
                    float radius = baseRadius * (circle - animationWavePropagationRefPoint);

                    var paint = new SkiaSharp.SKPaint
                    {
                        Style = SkiaSharp.SKPaintStyle.Stroke
                    };

                    paint.StrokeWidth = baseRadius / 2 * (circle == 0 ? animationWavePropagationRefPoint : 1);

                    paint.Color = new SkiaSharp.SKColor(colorRed, colorGreen, colorBlue, (byte)(255 * (circle == 5 ? animationWavePropagationRefPoint : 1)));

                    args.Surface.Canvas.DrawCircle(center.X, center.Y, radius, paint);
                }
            }
            else
            {
                for (int circle = 0; circle < 5; circle++)
                {
                    float radius = baseRadius * (circle + animationWavePropagationRefPoint);

                    var paint = new SkiaSharp.SKPaint
                    {
                        Style = SkiaSharp.SKPaintStyle.Stroke
                    };

                    paint.StrokeWidth = baseRadius / 2 * (circle == 0 ? animationWavePropagationRefPoint : 1);

                    paint.Color = new SkiaSharp.SKColor(colorRed, colorGreen, colorBlue, (byte)(255 * (circle == 4 ? (1 - animationWavePropagationRefPoint) : 1)));

                    args.Surface.Canvas.DrawCircle(center.X, center.Y, radius, paint);
                }
            }
        }

        private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs args)
        {
            cancellationTokenSource?.Cancel();

            cancellationTokenSource = null;

            StartSenderSearchIfValidNetworkAccess(args.NetworkAccess);
        }

        private void QrCodeScanButton_Clicked(object sender, System.EventArgs args)
        {
            StopSenderSearch();

            this.NavigationPushModalPageAsync(new QRCodeScanPage(OnQrCodeScanSuccess));
        }

        private void CloseButton_Clicked(object sender, System.EventArgs args)
        {
            DisplayAlert("Confirmar", "Está seguro que desea terminar el proceso Compartir con Amigos?", "Terminar", "Cancelar").ContinueWith(task =>
            {
                if (!task.Result) return;

                Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

                cancellationTokenSource?.Cancel();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    PerformSaveClose(null);
                });
            });
        }

        private void SenderCandidateTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var model = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.DataShareSenderViewModel;

            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new Popup.DataShareCandidateSenderDetailPopupPage(model, SenderCandidateApprovedCallback));
        }

        private void PerformSaveClose(System.Action afteAction)
        {
            var pageContext = BindingContext as Models.Page.DatabaseShareRecivePageModel;

            IsEnabled = false;

            if (pageContext.Sender != null)
            {
                Helpers.DataShareBundleReceiveHelper.Instance.CancelBundleSendListening();
            }

            pageContext.IsBusy = false;
            pageContext.BusyProgressMessage = null;
            pageContext.Sender = null;
            pageContext.Candidates.Clear();

            this.NavigationPopModalPageAsync();

            afteAction?.Invoke();
        }

        private void SenderCandidateApprovedCallback(Models.View.DataShareSenderViewModel target)
        {
            var pageContext = BindingContext as Models.Page.DatabaseShareRecivePageModel;

            if (target == null)
            {
                DisplayAlert("Aviso", "Este Amigo ya no está disponible para compartir sus datos.", "Aceptar");

                pageContext.Candidates.Remove(target);

                return;
            }

            cancellationTokenSource?.Cancel();

            IsEnabled = false;

            System.Threading.Tasks.Task.Delay(System.TimeSpan.FromMilliseconds(500)).ContinueWith(_ =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    pageContext.Sender = target;
                    pageContext.BusyProgressMessage = "Esperando envío del paquete de actualización ...";

                    pageContext.Candidates.Clear();

                    IsEnabled = true;
                });
            });
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
                        IsEnabled = false;

                        (BindingContext as Models.Page.DatabaseShareRecivePageModel).BusyProgressMessage = string.Format("Conectando a la red WiFi '{0}' ...", ssid);

                        wifiAccessManager.RegisterCallbackActions(OnWiFiAccessCallback);

                        wifiAccessManager.Connect(ssid, password);
                    }
                    else
                    {
                        DisplayAlert("Confirmar", string.Format("Está seguro que desea conectarse a la red WiFi '{0}'?", ssid), "Aceptar", "Cancelar").ContinueWith(task =>
                        {
                            if (!task.Result) return;

                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                IsEnabled = false;

                                (BindingContext as Models.Page.DatabaseShareRecivePageModel).BusyProgressMessage = string.Format("Conectando a la red WiFi '{0}' ...", ssid);
                            });

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

                IsEnabled = true;
            }
            else if (errorCode > 0)
            {
                DisplayAlert("Aviso", "No fue posible conectarse a la red WiFi especificada.", "Aceptar");

                IsEnabled = true;

                (BindingContext as Models.Page.DatabaseShareRecivePageModel).BusyProgressMessage = null;
            }
        }

        private async System.Threading.Tasks.Task ListenForSenderPresenceBroadcastAsync(System.Threading.CancellationToken cancellationToken)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(5));

            var broadcastingEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, Providers.ConstantsProvider.DATASHARE_SENDER_PRESENCE_BROADCAST_SOCKET_PORT);

            var referenceableBroadcastingEndPoint = (System.Net.EndPoint)broadcastingEndPoint;

            var broadcastingSocket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp);

            broadcastingSocket.Bind(broadcastingEndPoint);

            string broadcastMessage = null;

            while (!cancellationToken.IsCancellationRequested)
            {
                if (availableGridCoordenates.Count <= 0)
                {
                    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(60));

                    continue;
                }

                var broadcastContent = new byte[Providers.ConstantsProvider.BROADCAST_SOCKET_BUFFER_LENGHT];

                int broadcastContentLength = broadcastingSocket.ReceiveFrom(broadcastContent, ref referenceableBroadcastingEndPoint);

                broadcastMessage += System.Text.Encoding.UTF8.GetString(broadcastContent, 0, broadcastContentLength);

                if (broadcastMessage.IndexOf(Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK) > -1)
                {
                    var jsonData = broadcastMessage.Substring(0, broadcastMessage.Length - Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK.Length);

                    var senderFriend = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Data.FriendDataModel>(jsonData);

                    var targetGridCoordenate = availableGridCoordenates.Dequeue();

                    broadcastMessage = null;

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        var pageContext = BindingContext as Models.Page.DatabaseShareRecivePageModel;

                        if (pageContext!= null && pageContext.Sender == null && !pageContext.Candidates.Any(item => item.Friend.Id == senderFriend.Id))
                        {
                            pageContext.Candidates.Add(new Models.View.DataShareSenderViewModel
                            {
                                IPAddress = (referenceableBroadcastingEndPoint as System.Net.IPEndPoint).Address,
                                Friend = senderFriend,
                                HumanizeDataBundleLength = senderFriend.DataLength.ToFileSizeString(),
                                GridColumn = targetGridCoordenate.Column,
                                GridRow = targetGridCoordenate.Row
                            });
                        }
                    });

                    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(5));
                }
            }

            broadcastingSocket.Close();

            broadcastingSocket.Dispose();
        }

        private void OnListenForSenderBroadcastCallback(System.Threading.Tasks.Task task)
        {
            if (task.Exception == null)
            {
                var pageContext = BindingContext as Models.Page.DatabaseShareRecivePageModel;

                if (pageContext.Sender == null) return;

                System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        Helpers.DataShareBundleReceiveHelper.Instance.ListenForBundleSend(pageContext, bundleContent =>
                        {
                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                if (bundleContent.LongCount() == pageContext.Sender.Friend.DataLength)
                                {
                                    Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

                                    cancellationTokenSource?.Cancel();

                                    var at = pageContext.Sender.Friend.DataUpdatedAt;

                                    PerformSaveClose(() =>
                                    {
                                        shareDataReceivedCallbackAction?.Invoke(bundleContent.ToArray(), at);

                                        shareDataReceivedCallbackAction = null;
                                    });
                                }
                                else
                                {
                                    DisplayAlert("Aviso", "Error recibiendo paquete de actualización.", "Reintentar", "Cancelar").ContinueWith(alertTask =>
                                    {
                                        if (alertTask.Result)
                                        {
                                            pageContext.Sender = null;

                                            StartSenderSearchIfValidNetworkAccess(Xamarin.Essentials.Connectivity.NetworkAccess);
                                        }
                                        else
                                        {
                                            Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

                                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                                            {
                                                PerformSaveClose(null);
                                            });
                                        }
                                    });
                                }
                            });
                        });
                        //ListenForBundleSend(cancellationTokenSource.Token);
                    }
                    catch (System.Exception exception)
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            if (!(exception is System.Net.Sockets.SocketException && !IsEnabled))
                            {
                                // TODO Reempazar el mensaje de error!
                                DisplayAlert("Aviso", "Error recibiendo paquete de actualización.", "Reintentar", "Cancelar").ContinueWith(alertTask =>
                                {
                                    if (alertTask.Result)
                                    {
                                        pageContext.Sender = null;

                                        StartSenderSearchIfValidNetworkAccess(Xamarin.Essentials.Connectivity.NetworkAccess);
                                    }
                                    else
                                    {
                                        Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

                                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                                        {
                                            PerformSaveClose(null);
                                        });
                                    }
                                });
                            }
                        });
                    }
                });

                return;
            }

            // TODO Reempazar el mensaje de error!
            DisplayAlert("Aviso", "Error buscando amigos que desean compartir sus datos.", "Reintentar", "Cancelar").ContinueWith(alertTask =>
            {
                if (alertTask.Result)
                {
                    StartSenderSearchIfValidNetworkAccess(Xamarin.Essentials.Connectivity.NetworkAccess);
                }
                else
                {
                    Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        PerformSaveClose(null);
                    });
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
                pageContext.BusyProgressMessage = null;
                pageContext.Sender = null;
                pageContext.Candidates.Clear();

                return;
            }

            var candiateGridCoordinates = new System.Collections.Generic.List<Models.GridCoordinateModel>();

            for (int column = 0; column < 5; column++)
            {
                for (int row = 0; row < 5; row++)
                {
                    if (column == 2 && row == 2) continue;
                    if (column == 0 && row == 0) continue;
                    if (column == 0 && row == 4) continue;
                    if (column == 4 && row == 0) continue;
                    if (column == 4 && row == 4) continue;

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

            pageContext.BusyProgressMessage = "Buscando grupos de amigos ...";

            EnableReciveDataAnimation();

            IsEnabled = true;

            cancellationTokenSource = new System.Threading.CancellationTokenSource();

            System.Threading.Tasks.Task.Run(async () =>
            {
                await ListenForSenderPresenceBroadcastAsync(cancellationTokenSource.Token).ContinueWith(OnListenForSenderBroadcastCallback);
            });
        }

        private void StopSenderSearch()
        {
            cancellationTokenSource?.Cancel();

            (BindingContext as Models.Page.DatabaseShareRecivePageModel).IsBusy = false;

            Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        private void EnableReciveDataAnimation()
        {
            stopwatch.Start();

            var pageContext = BindingContext as Models.Page.DatabaseShareRecivePageModel;

            pageContext.IsBusy = true;

            Xamarin.Forms.Device.StartTimer(System.TimeSpan.FromMilliseconds(33), () =>
            {
                animationWavePropagationRefPoint = (float)(stopwatch.Elapsed.TotalMilliseconds % CYCLE_TIME_MILLISECOUNDS / CYCLE_TIME_MILLISECOUNDS);

                CanvasView.InvalidateSurface();

                var @continue = pageContext != null && pageContext.IsBusy;

                if (!@continue)
                {
                    stopwatch.Stop();
                }

                if (pageContext.Sender != null && !pageContext.IsReceivingBundle)
                {
                    if (receiverPrecenceExpirationRefPoint > RECEIVER_EXPIRE_TIME)
                    {
                        System.Threading.Tasks.Task.Run(() =>
                        {
                            var hostFriendDataModel = Factories.FriendDataModelFactory.CreateSelf();

                            var receiverPresenceMessageContent = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}{1}", Newtonsoft.Json.JsonConvert.SerializeObject(hostFriendDataModel), Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK));

                            var receiverPresenceNotifyEndPoint = new System.Net.IPEndPoint(pageContext.Sender.IPAddress, Providers.ConstantsProvider.DATASHARE_RECEIVER_PRESENCE_UNICAST_SOCKET_PORT);

                            var receiverPresenceNotifySocket = new System.Net.Sockets.Socket(pageContext.Sender.IPAddress.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

                            receiverPresenceNotifySocket.Connect(receiverPresenceNotifyEndPoint);

                            receiverPresenceNotifySocket.Send(receiverPresenceMessageContent);

                            receiverPresenceNotifySocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                            receiverPresenceNotifySocket.Close();
                        }).ContinueWith(task =>
                        {
                            // TODO Handle exception if is throwed!
                        });

                        receiverPrecenceExpirationRefPoint = System.TimeSpan.Zero;
                    }
                    else
                    {
                        receiverPrecenceExpirationRefPoint += TIMER_TIME;
                    }
                }
                else
                {
                    receiverPrecenceExpirationRefPoint = System.TimeSpan.Zero;
                }

                return @continue;
            });
        }
    }
}