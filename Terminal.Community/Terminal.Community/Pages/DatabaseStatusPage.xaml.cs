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

            BindingContext = new Models.Page.DatabaseStatusPageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ReloadDatabaseSummary();
        }

        private void UpdateDatabaseButton_Clicked(object sender, System.EventArgs e)
        {
            var forceUpgrade = !Xamarin.Forms.Application.Current.Properties.ContainsKey(Providers.ConstantsProvider.API_KEY_SECRET_PROPERTY_KEY) || !Providers.ConstantsProvider.API_KEY_SECRET.Equals(Xamarin.Forms.Application.Current.Properties[Providers.ConstantsProvider.API_KEY_SECRET_PROPERTY_KEY]);

            this.NavigationPushModalPageAsync(new DatabaseUpdatePage(forceUpgrade));
        }

        private void ShareDatabaseButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            // TODO Restore to default behavior!!
            //if (!updateForceModeRequired)

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
                                this.NavigationPushModalPageAsync(new DatabaseShareReceivePage());

                                break;
                        }
                    });

                });

                return;
            }
            else
            {
                this.NavigationPushModalPageAsync(new DatabaseShareReceivePage());
            }
        }

        private void ClearDatabaseButton_Clicked(object sender, System.EventArgs e)
        {
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

        private void ReloadDatabaseSummary()
        {
            var model = BindingContext as Models.Page.DatabaseStatusPageModel;

            model.IsBusy = true;

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

                    var databaseFileInfo = new System.IO.FileInfo(Helpers.LocalFileStorageHelper.GetDatabaseFilePath());

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        model.Updated = true;
                        model.CinemaCount = cinemaCount;
                        model.RoomCount = roomCount;
                        model.PresentationCount = presentationCount;
                        model.ImageCount = imageCount;
                        model.EventCount = eventCount;
                        model.PersonalityCount = personalityCount;
                        model.CommentCount = commentCount;
                        model.Status = string.Format("{0}, {1} ({2})", syncronizationDate.ToString("dddd, MMMM dd"), syncronizationDate.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture), databaseFileInfo.Length.ToFileSizeString());
                    });
                }
            }).ContinueWith((System.Threading.Tasks.Task task) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    model.IsBusy = false;

                    if (task.Exception != null)
                    {
                        model.Status = null;
                        model.Updated = false;
                        model.CinemaCount = 0;
                        model.RoomCount = 0;
                        model.PresentationCount = 0;
                        model.ImageCount = 0;
                        model.EventCount = 0;
                        model.PersonalityCount = 0;
                        model.CommentCount = 0;
                    }
                });
            });
        }
    }
}