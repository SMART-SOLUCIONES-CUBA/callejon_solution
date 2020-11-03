using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class MasterPage : Xamarin.Forms.ContentPage
    {
        private const string FETCH_ORDER_AUDIO_VISUAL_OPTION_DETAIL = "Mostrar Detalles";
        private const string FETCH_ORDER_AUDIO_VISUAL_OPTION_QUIT = "Quitar del Carro de Compras";

        public MasterPage()
        {
            InitializeComponent();

            Xamarin.Forms.DependencyService.Get<Managers.Facade.IStorageTargetManagerFacade>().ConfigurateCallbacks(InvalidateTargetStorageOnMainThread);
        }

        private Models.Page.MasterPageModel BindingContextModel
        {
            get
            {
                return BindingContext as Models.Page.MasterPageModel;
            }
        }

        private void InvalidateTargetStorageOnMainThread()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                var storageTargetManager = Xamarin.Forms.DependencyService.Get<Managers.Facade.IStorageTargetManagerFacade>();

                var storage = storageTargetManager.GetTargetStorage();

                var presentedDetailPage = ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage).CurrentPage;

                if (presentedDetailPage is HomePage)
                {
                    foreach (var item in BindingContextModel.FetchOrderAudioVisuals)
                    {
                        (presentedDetailPage as Base.FetchOrderAudioVisualPresentBaseContentPage).OnAudioViualFetchMarkToogle(item);
                    }

                    BindingContextModel.FetchOrderAudioVisuals.Clear();
                }
                else
                {
                    BindingContextModel.FetchOrderAudioVisuals.Clear();

                    (Xamarin.Forms.Application.Current.MainPage as MainPage).PivotToHomePage();
                }

                BindingContextModel.Target = new Models.View.StorageViewModel
                {
                    Storage = storage,
                    FamilyRepresentativeIconGlyph = storage?.Family.ToFamilyRepresentativeIconGlyph() ?? "\uf028",
                    CurrentUsedPercentFactor = storage?.UsedSpace / storage?.TotalSpace ?? 0.00d
                };

                if (storage == null)
                {
                    var currentModalPage = (Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail.Navigation.ModalStack.LastOrDefault();

                    if (currentModalPage != null && currentModalPage is DownloadCenterPage)
                    {
                        (Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail.NavigationPopModalPageAsync();
                    }
                }

                InvalidateTargetStorageStats();
            });
        }

        public void InvalidateTargetStorageStats()
        {
            var storageTargetManager = Xamarin.Forms.DependencyService.Get<Managers.Facade.IStorageTargetManagerFacade>();

            BindingContextModel.Target.Storage = storageTargetManager.GetTargetStorage();
            BindingContextModel.Target.CurrentUsedPercentFactor = BindingContextModel.Target.Storage?.UsedSpace / BindingContextModel.Target.Storage?.TotalSpace ?? 0.00d;

            StorageSpaceReferenceBoxView.InvalidateWidth();
            VirtualStoreUsedSpaceSpan.InvalidateText();
        }

        public void PresentForStoragePick()
        {
            var storageTargetManager = Xamarin.Forms.DependencyService.Get<Managers.Facade.IStorageTargetManagerFacade>();

            storageTargetManager.GetAvailableStoragesAsync().ContinueWith(getAvailableStoragesTask =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (getAvailableStoragesTask.Exception != null)
                    {
                        DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, "No fue posible listar los dispositivos de almacenamiento disponibles.", Providers.Constant.StringConstantProvider.OK_ACTION);
                        //DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, string.Format("Error: {1}({2}).{0}{0}Traza:{3}.", System.Environment.NewLine, getAvailableStoragesTask.Exception.InnerException.Message, getAvailableStoragesTask.Exception.InnerException.GetType().FullName, getAvailableStoragesTask.Exception.InnerException.StackTrace), Providers.Constant.StringConstantProvider.OK_ACTION);
                    }
                    else
                    {
                        DisplayActionSheet("Seleccione dispositivo de almacenamiento:", Providers.Constant.StringConstantProvider.CANCEL_ACTION, Providers.Constant.StringConstantProvider.MALE_NONE_OPTION, getAvailableStoragesTask.Result.Select(item => item.Caption).ToArray()).ContinueWith(pickTask =>
                        {
                            switch (pickTask.Result)
                            {
                                case Providers.Constant.StringConstantProvider.MALE_NONE_OPTION:
                                    storageTargetManager.TargetStorage(null);

                                    InvalidateTargetStorageOnMainThread();

                                    break;
                                case Providers.Constant.StringConstantProvider.CANCEL_ACTION:
                                    break;

                                case null:
                                    break;

                                default:
                                    storageTargetManager.TargetStorage(getAvailableStoragesTask.Result.First(item => item.Caption == pickTask.Result));

                                    InvalidateTargetStorageOnMainThread();

                                    break;
                            }
                        });
                    }
                });                
            });
        }

        public void HandleFetchOrderDownloadProgressPresentation()
        {
            Xamarin.Forms.DependencyService.Get<Helpers.Facade.IFetchOrderDownloadRequestHelper>().BindCallbacks(FetchOrderDownloadRequeastEnd_Callback, FetchOrderDownloadRequestProgress_Callback);

            MainScrollView.ScrollToAsync(0, 0, true).ContinueWith(_ => { });

            BindingContextModel.IsBusy = true;            
        }

        private void PickStorageDeviceButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (BindingContextModel.FetchOrderAudioVisuals.Count > 0)
            {
                DisplayAlert(Providers.Constant.StringConstantProvider.CONFIRMATION_ALERT_DIALOG_TITLE, string.Format("Al cambiar de dispositivo de almacenamiento se retirarán los elementos en el carro de compras.{0}{0}Está seguro que desea continuar?", System.Environment.NewLine), Providers.Constant.StringConstantProvider.OK_ACTION, Providers.Constant.StringConstantProvider.CANCEL_ACTION).ContinueWith(task =>
                {
                    if (!task.Result) return;

                    PresentForStoragePick();
                });
            }
            else
            {
                PresentForStoragePick();
            }
        }

        private void FullShoppingCartButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (BindingContextModel.Target.Storage == null)
            {
                DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, Providers.Constant.StringConstantProvider.UNSPECIFIED_STORAGE_DEVICE_EXCEPTION_MESSAGE, Providers.Constant.StringConstantProvider.OK_ACTION);

                return;
            }

            (Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).NavigationPushModalPageAsync(new DownloadCenterPage(), false);
        }

        private void FetchOrderAudioVisualOptionButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var model = (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel;

            DisplayActionSheet("Opciones", "Cancelar", null, FETCH_ORDER_AUDIO_VISUAL_OPTION_DETAIL, FETCH_ORDER_AUDIO_VISUAL_OPTION_QUIT).ContinueWith(task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    switch (task.Result)
                    {
                        case FETCH_ORDER_AUDIO_VISUAL_OPTION_DETAIL:
                            var detailNavigationPage = (Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage;

                            if (detailNavigationPage.CurrentPage is AudioVisualDetailPage)
                            {
                                if ((detailNavigationPage.CurrentPage as AudioVisualDetailPage).BindingContextModel.AudioVisual.Id == model.Id)
                                {
                                    break;
                                }
                            }

                            detailNavigationPage.PushAsync(new AudioVisualDetailPage(new Models.View.AudioVisualViewModel
                            {
                                AudioVisual = model,
                                IsMarkedForFetch = true
                            }, detailNavigationPage.CurrentPage as Base.FetchOrderAudioVisualPresentBaseContentPage));

                            break;

                        case FETCH_ORDER_AUDIO_VISUAL_OPTION_QUIT:
                            Xamarin.Forms.Application.Current.RemoveAudioVisualForFetch(model);

                            var presentedPage = ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage).CurrentPage;

                            if (presentedPage is Base.FetchOrderAudioVisualPresentBaseContentPage)
                            {
                                (presentedPage as Base.FetchOrderAudioVisualPresentBaseContentPage).OnAudioViualFetchMarkToogle(model);
                            }

                            break;
                    }
                });
            });            
        }

        private void FetchOrderDownloadRequeastEnd_Callback(string orderFolderName, System.TimeSpan elapsedTime, System.Exception exception)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                BindingContextModel.BusyProgressMessage = "Actualizando ...";

                BindingContextModel.DownloadingIndex = 0;
            });

            Xamarin.Forms.DependencyService.Get<Managers.Facade.IStorageTargetManagerFacade>().ReloadTargetStorageAsync().ContinueWith(_ =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (exception != null)
                    {
                        // TODO Delete incomplete download FetchOrder

                        XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception);
                    }
                    else
                    {
                        var timeSpanToFullStringConverter = new Converters.TimeSpanToFullStringConverter();

                        DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, string.Format("Lista de descarga procesada con éxito en {1}.{0}{0}Los archivos descargados podrá encontrarlos en el dispositivo '{2}' carpeta 'Mediateca Nuevo Mundo' subcarpeta '{3}'.{0}{0}Puede continuar explorando nuestros contenidos y realizar nuevas descargas si así lo desea.", System.Environment.NewLine, timeSpanToFullStringConverter.Convert(elapsedTime, typeof(System.TimeSpan), null, System.Globalization.CultureInfo.CurrentCulture), BindingContextModel.MasterBindingContext.Target.Storage.Caption, orderFolderName), Providers.Constant.StringConstantProvider.OK_ACTION);

                        var donwloadCenterDependPage = ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage).CurrentPage as Base.FetchOrderAudioVisualPresentBaseContentPage;

                        var items = BindingContextModel.MasterBindingContext.FetchOrderAudioVisuals.ToArray();

                        foreach (var item in items)
                        {
                            BindingContextModel.MasterBindingContext.FetchOrderAudioVisuals.Remove(item);

                            donwloadCenterDependPage?.OnAudioViualFetchMarkToogle(item);
                        }

                        BindingContextModel.MasterBindingContext.RaiseFetchOrderAudioVisualsChanged();
                    }

                    Xamarin.Forms.DependencyService.Get<Helpers.Facade.IFetchOrderDownloadRequestHelper>().UnbindCallbacks();

                    InvalidateTargetStorageStats();

                    BindingContextModel.IsBusy = false;
                });
            });
        }

        private void FetchOrderDownloadRequestProgress_Callback(int index, long transferProgress, long transferLength, System.TimeSpan elapsedTime)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                BindingContextModel.BusyProgressMessage = "Descargando ...";

                BindingContextModel.DownloadingIndex = index;
                BindingContextModel.Time = elapsedTime;
                BindingContextModel.TransferProgress = transferProgress;
                BindingContextModel.TransferLength = transferLength;
                BindingContextModel.ProgressPercent = decimal.Round(new decimal(transferProgress) / new decimal(transferLength), 2);
            });
        }
    }
}