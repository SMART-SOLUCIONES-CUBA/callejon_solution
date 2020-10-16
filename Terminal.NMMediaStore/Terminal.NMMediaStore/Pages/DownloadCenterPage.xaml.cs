using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class DownloadCenterPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private System.Diagnostics.Stopwatch stopwatch;

        public DownloadCenterPage()
        {
            InitializeComponent();

            BindingContext = new Models.Page.DownloadCenterPageModel
            {
                MasterBindingContext = (Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Master.BindingContext as Models.Page.MasterPageModel
            };
        }

        private Models.Page.DownloadCenterPageModel BindingContextModel
        {
            get
            {
                return BindingContext as Models.Page.DownloadCenterPageModel;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            TranslationX = Width * -1;

            var animation = new XFToolKit.Presentation.Transform.Animations.TranslateToXAnimation
            {
                EasingType = XFToolKit.Presentation.Transform.Enumerations.EasingType.CubicOut,
                Target = this,
                TranslateX = 0,
                Duration = "500"
            };

            animation.Begin();
        }

        private void BackButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPopModalPageAsync();
        }

        private void FetchOrderAudioVisualItemRemoveButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            DisplayAlert("Confirmar", "Esta seguro que desea eliminar este Audio Visual del Carro de Compras?", "Eliminar", "Cancelar").ContinueWith(task =>
            {
                if (!task.Result) return;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var model = (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel;

                    BindingContextModel.MasterBindingContext.FetchOrderAudioVisuals.Remove(model);

                    ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Master as MasterPage).InvalidateTargetStorageStats();

                    TargetStorageVirtualTotalSpaceBoxView.InvalidateWidth();
                    VirtualStoreUsedSpaceSpan.InvalidateText();

                    BindingContextModel.MasterBindingContext.RaiseFetchOrderAudioVisualsChanged();

                    var presentedPage = ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage).CurrentPage;

                    if (presentedPage is Base.FetchOrderAudioVisualPresentBaseContentPage)
                    {
                        (presentedPage as Base.FetchOrderAudioVisualPresentBaseContentPage).OnAudioViualFetchMarkToogle(model);
                    }
                });
            });
        }

        private void FetchOrderCompleteButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            BindingContextModel.IsBusy = true;
            BindingContextModel.BusyProgressMessage = "Registrando orden ...";

            System.Threading.Tasks.Task.Run(async () => 
            {
                var orderId = await XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.IFetchOrderManagerFacade>().Create(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.FetchOrderModel
                {
                    SubscriptorId = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>().GetAuthenticatedSubscritor().Subscription.Id,
                    AudioVisuals = BindingContextModel.MasterBindingContext.FetchOrderAudioVisuals.ToList()
                });

                stopwatch = new System.Diagnostics.Stopwatch();

                stopwatch.Start();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    BindingContextModel.BusyProgressMessage = "Inicando ...";
                });

                await Xamarin.Forms.DependencyService.Get<Helpers.Facade.IFetchOrderDownloadRequestHelper>().RequestAsync(orderId, BindingContextModel.MasterBindingContext.Target.Storage.Path, FetchOrderDownloadRequeastEnd_Callback, FetchOrderDownloadRequestProgress_Callback);                
            }).ContinueWith(task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (task.Exception != null)
                    {
                        // TODO Delete incomplete download FetchOrder

                        stopwatch.Stop();

                        stopwatch = null;

                        XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(task.Exception);
                    }
                });
            });
        }

        private void FetchOrderDownloadRequeastEnd_Callback(string orderFolderName, System.Exception exception)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                stopwatch.Stop();

                if (exception != null)
                {
                    // TODO Delete incomplete download FetchOrder

                    ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Master as MasterPage).InvalidateTargetStorageStats();

                    TargetStorageVirtualTotalSpaceBoxView.InvalidateWidth();
                    VirtualStoreUsedSpaceSpan.InvalidateText();

                    XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception);
                }
                else
                {
                    var timeSpanToFullStringConverter = new Converters.TimeSpanToFullStringConverter();

                    DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, string.Format("Lista de descarga procesada con éxito en {1}.{0}{0}Los archivos descargados podrá encontrarlos en el dispositivo '{2}' carpeta 'Mediateca Nuevo Mundo' subcarpeta '{3}'.{0}{0}Puede continuar explorando nuestros contenidos y realizar nuevas descargas si así lo desea.", System.Environment.NewLine, timeSpanToFullStringConverter.Convert(stopwatch.Elapsed, typeof(System.TimeSpan), null, System.Globalization.CultureInfo.CurrentCulture), BindingContextModel.MasterBindingContext.Target.Storage.Caption, orderFolderName), Providers.Constant.StringConstantProvider.OK_ACTION);

                    var donwloadCenterDependPage = ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage).CurrentPage as Base.FetchOrderAudioVisualPresentBaseContentPage;

                    var items = BindingContextModel.MasterBindingContext.FetchOrderAudioVisuals.ToArray();

                    foreach (var item in items)
                    {
                        BindingContextModel.MasterBindingContext.FetchOrderAudioVisuals.Remove(item);

                        donwloadCenterDependPage?.OnAudioViualFetchMarkToogle(item);
                    }

                    ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Master as MasterPage).InvalidateTargetStorageStats();

                    TargetStorageVirtualTotalSpaceBoxView.InvalidateWidth();
                    VirtualStoreUsedSpaceSpan.InvalidateText();

                    BindingContextModel.MasterBindingContext.RaiseFetchOrderAudioVisualsChanged();

                    this.NavigationPopModalPageAsync();
                }

                stopwatch = null;
            });
        }

        private void FetchOrderDownloadRequestProgress_Callback(int index, long transferProgress, long transferLength)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                BindingContextModel.BusyProgressMessage = "Descargando ...";

                BindingContextModel.DownloadingIndex = index;
                BindingContextModel.Time = stopwatch.Elapsed;
                BindingContextModel.TransferProgress = transferProgress;
                BindingContextModel.TransferLength = transferLength;
                BindingContextModel.ProgressPercent = decimal.Round(new decimal(transferProgress) / new decimal(transferLength), 2);
            });
        }
    }
}