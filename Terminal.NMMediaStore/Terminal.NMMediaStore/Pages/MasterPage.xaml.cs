using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;
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

                var storage = storageTargetManager.GetStorageTarget();

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

                    if (currentModalPage != null && currentModalPage is ShoppingCartPage)
                    {
                        (Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail.Navigation.PopModalAsync();
                    }
                }

                InvalidateTargetStorageStats();
            });
        }

        public void InvalidateTargetStorageStats()
        {
            StorageSpaceReferenceBoxView.InvalidateWidth();
            VirtualStoreUsedSpaceSpan.InvalidateText();
        }

        private void PresentForStoragePick()
        {
            var storageTargetManager = Xamarin.Forms.DependencyService.Get<Managers.Facade.IStorageTargetManagerFacade>();

            storageTargetManager.GetAvailableStoragesAsync().ContinueWith(getAvailableStoragesTask =>
            {
                if (getAvailableStoragesTask.Exception != null)
                {
                    DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, "No fue posible listar los dispositivos de almacenamiento disponibles.", Providers.Constant.StringConstantProvider.OK_ACTION);
                }
                else
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
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
                                default:
                                    storageTargetManager.TargetStorage(getAvailableStoragesTask.Result.First(item => item.Caption == pickTask.Result));

                                    InvalidateTargetStorageOnMainThread();

                                    break;
                            }
                        });
                    });
                }
            });
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

            ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage).Navigation.PushModalAsync(new ShoppingCartPage(), false);
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
    }
}