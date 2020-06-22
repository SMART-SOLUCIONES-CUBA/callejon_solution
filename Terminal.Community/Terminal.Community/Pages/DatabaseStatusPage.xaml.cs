using SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Extensions;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DatabaseStatusPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private const string SEND_SHARE_ACTION_CAPTION = "Crear Grupo de Amigos";
        private const string RECEIVE_SHARE_ACTION_CAPTION = "Unirse a Grupo de Amigos";

        public DatabaseStatusPage()
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.DatabaseStatusPageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Xamarin.Forms.DependencyService.Get<Managers.Facade.IDisplayOrientationManagerFacade>()?.Unlock();

            Xamarin.Essentials.DeviceDisplay.KeepScreenOn = false;

            if (IsEnabled) return;

            ReloadDatabaseSummary();

            IsEnabled = true;
        }

        private void UpdateDatabaseButton_Clicked(object sender, System.EventArgs e)
        {
            var forceUpgrade = !Xamarin.Forms.Application.Current.Properties.ContainsKey(Providers.ConstantsProvider.API_KEY_SECRET_PROPERTY_KEY) || !Providers.ConstantsProvider.API_KEY_SECRET.Equals(Xamarin.Forms.Application.Current.Properties[Providers.ConstantsProvider.API_KEY_SECRET_PROPERTY_KEY]);

            this.NavigationPushModalPageAsync(new DatabaseUpdatePage(forceUpgrade));
        }

        private void ShareDatabaseButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (default != Helpers.DataSyncronizationHelper.Instance.SyncronizationDate)
            {
                if (Xamarin.Forms.DependencyService.Get<Managers.Facade.IWiFiHostpotManagerFacade>() != null)
                {
                    DisplayActionSheet("Compartir Datos", "Cancelar", null, SEND_SHARE_ACTION_CAPTION, RECEIVE_SHARE_ACTION_CAPTION).ContinueWith(task =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            switch (task.Result)
                            {
                                case SEND_SHARE_ACTION_CAPTION:
                                    this.NavigationPushModalPageAsync(new DatabaseShareSendPage());

                                    break;

                                case RECEIVE_SHARE_ACTION_CAPTION:
                                    this.NavigationPushModalPageAsync(new DatabaseShareReceivePage(ShareDatabaseReceived__Callback));

                                    break;
                            }
                        });

                    });
                }
                else
                {
                    this.NavigationPushModalPageAsync(new DatabaseShareReceivePage(ShareDatabaseReceived__Callback));
                }
            }
            else
            {
                this.NavigationPushModalPageAsync(new DatabaseShareReceivePage(ShareDatabaseReceived__Callback));
            }
        }

        private void ClearDatabaseButton_Clicked(object sender, System.EventArgs e)
        {
            if (default == Helpers.DataSyncronizationHelper.Instance.SyncronizationDate)
            {
                DisplayAlert("Aviso", "No existen datos almacenados de actualizaciones.", "Aceptar");

                return;
            }

            DisplayAlert("Confirmar", "Está seguro que desea elimnar la actualización de datos actual?", "Elimnar", "Cancelar").ContinueWith(displayAlertTask =>
            {
                if (!displayAlertTask.Result) return;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.DatabaseStatusPageModel).IsBusy = true;

                    System.Threading.Tasks.Task.Run(async () =>
                    {
                        Helpers.LocalFileStorageHelper.DeleteLocalFiles();

                        await Helpers.DataSyncronizationHelper.Instance.ResetAsync();
                    }).ContinueWith((System.Threading.Tasks.Task clearDatabaseActionTask) =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            ReloadDatabaseSummary();

                            DisplayAlert("Aviso", "Actualización eliminada con éxito.", "Aceptar");
                        });
                    });
                });
            });
        }

        private void ShareDatabaseReceived__Callback(byte[] bundleContent, System.DateTime at)
        {
            if (bundleContent == null) return;

            DisplayAlert("Confirmar", string.Format("Paquete de actualización recibido con éxito.{0}{0}Está seguro que desea que la aplicación use los datos recibidos?", System.Environment.NewLine), "Usar Datos", "Cancelar").ContinueWith(async dialogTask =>
            {
                if (!dialogTask.Result) return;

                var pageContext = BindingContext as Models.Page.DatabaseStatusPageModel;

                pageContext.IsBusy = true;
                pageContext.BusyProgressMessage = "Aplicando paquede de actualización compartido ...";

                Helpers.LocalFileStorageHelper.DeleteLocalFiles();

                await Helpers.DataSyncronizationHelper.Instance.ResetAsync();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    Helpers.DataSyncronizationHelper.Instance.StartDataBundleAppling(bundleContent, at, applingTask =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            pageContext.IsBusy = false;
                            pageContext.BusyProgressMessage = null;

                            if (applingTask.Exception == null)
                            {
                                DisplayAlert("Aviso", "Paquete de actualización aplicado con éxito.", "Aceptar");

                                ReloadDatabaseSummary();
                            }
                            else
                            {
                                DisplayAlert("Aviso", string.Format("No fue posible aplicar el paquete de actualización.{0}{0}El contenido del archivo recibido no está completo.", System.Environment.NewLine), "Aceptar");
                            }
                        });
                    });
                });
            });
        }

        private void ReloadDatabaseSummary()
        {
            var pgeContext = BindingContext as Models.Page.DatabaseStatusPageModel;

            pgeContext.IsBusy = true;

            System.Threading.Tasks.Task.Run(() =>
            {
                var syncronizationDate = Helpers.DataSyncronizationHelper.Instance.SyncronizationDate;

                using (var dbContext = Providers.DdContextProvider.GetDbContext())
                {
                    var cinemaCount = dbContext.Cinemas.Count();
                    var roomCount = dbContext.Rooms.Count();
                    var presentationCount = dbContext.Presentations.Count();
                    var imageCount = dbContext.Images.Count();
                    var eventCount = dbContext.Events.Count();
                    var personalityCount = dbContext.Personalities.Count();
                    var commentCount = dbContext.EventComments.Count() + dbContext.PersonalityComments.Count() + dbContext.ProductionComments.Count();

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        pgeContext.Updated = true;
                        pgeContext.CinemaCount = cinemaCount;
                        pgeContext.RoomCount = roomCount;
                        pgeContext.PresentationCount = presentationCount;
                        pgeContext.ImageCount = imageCount;
                        pgeContext.EventCount = eventCount;
                        pgeContext.PersonalityCount = personalityCount;
                        pgeContext.CommentCount = commentCount;
                        pgeContext.Status = string.Format("{0}, {1} ({2})", syncronizationDate.ToString("dddd, MMMM dd"), syncronizationDate.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture), Helpers.LocalFileStorageHelper.GetLocalFileDataSize().ToFileSizeString());
                    });
                }
            }).ContinueWith((System.Threading.Tasks.Task task) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    pgeContext.IsBusy = false;

                    if (task.Exception != null)
                    {
                        pgeContext.Status = null;
                        pgeContext.Updated = false;
                        pgeContext.CinemaCount = 0;
                        pgeContext.RoomCount = 0;
                        pgeContext.PresentationCount = 0;
                        pgeContext.ImageCount = 0;
                        pgeContext.EventCount = 0;
                        pgeContext.PersonalityCount = 0;
                        pgeContext.CommentCount = 0;
                    }
                });
            });
        }
    }
}