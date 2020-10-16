using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class AudioVisualDetailPage : Base.FetchOrderAudioVisualPresentBaseContentPage
    {
        private Base.FetchOrderAudioVisualPresentBaseContentPage navigationParent;

        public AudioVisualDetailPage(Models.View.AudioVisualViewModel model, Base.FetchOrderAudioVisualPresentBaseContentPage navigationParent)
        {
            this.navigationParent = navigationParent;

            InitializeComponent();

            BindingContext = new Models.Page.AudioVisualDetailPageModel
            {
                IsMarkedForFetch = model.IsMarkedForFetch,
                AudioVisual = model.AudioVisual,
                IsBusy = true
            };
        }

        public Models.Page.AudioVisualDetailPageModel BindingContextModel
        {
            get
            {
                return BindingContext as Models.Page.AudioVisualDetailPageModel;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            System.Threading.Tasks.Task.Run(async () =>
            {
                var subscriptionManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>();
                var fetchOrderManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.IFetchOrderManagerFacade>();
                var productionCheckoutManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.IProductionCheckoutManagerFacade>();
                var commentManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ICommentManagerFacade>();

                await productionCheckoutManager.AddAsync(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProductionCkeckoutModel
                {
                    ProductionId = BindingContextModel.AudioVisual.Id,
                    SubscriptorId = subscriptionManager.GetAuthenticatedSubscritor().Subscription.Id
                });

                var checkoutCount = await productionCheckoutManager.CountByProductionIdAsync(BindingContextModel.AudioVisual.Id);
                var downloadCount = await fetchOrderManager.CountByProductionIdAsync(BindingContextModel.AudioVisual.Id);
                var commentCount = await commentManager.CountByProductionIdAsync(BindingContextModel.AudioVisual.Id);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (BindingContextModel == null) return;

                    BindingContextModel.CheckCount = checkoutCount;
                    BindingContextModel.DownloadCount = downloadCount;
                    BindingContextModel.CommentCount = commentCount;

                    BindingContextModel.IsBusy = false;
                });
            });
        }

        public override void OnAudioViualFetchMarkToogle(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel target)
        {
            if (target.Id == BindingContextModel.AudioVisual.Id)
            {
                BindingContextModel.IsMarkedForFetch = !BindingContextModel.IsMarkedForFetch;
            }

            navigationParent.OnAudioViualFetchMarkToogle(target);
        }

        private void AlternativeBackButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPopPageAsync();
        }

        private void FetchMarkToogleButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            try
            {
                if (BindingContextModel.IsMarkedForFetch)
                {
                    Xamarin.Forms.Application.Current.RemoveAudioVisualForFetch(BindingContextModel.AudioVisual);
                }
                else
                {
                    Xamarin.Forms.Application.Current.AddAudioVisualForFetch(BindingContextModel.AudioVisual);
                }

                BindingContextModel.IsMarkedForFetch = !BindingContextModel.IsMarkedForFetch;

                navigationParent.OnAudioViualFetchMarkToogle(BindingContextModel.AudioVisual);
            }
            catch (Exceptions.UnspecifiedStorageDeviceException)
            {
                DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, Providers.Constant.StringConstantProvider.UNSPECIFIED_STORAGE_DEVICE_EXCEPTION_MESSAGE, Providers.Constant.StringConstantProvider.OK_ACTION);
            }
            catch (Exceptions.InsufficientStorageSpaceException)
            {
                DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, Providers.Constant.StringConstantProvider.INSUFFICIENT_STORAGE_SPACE_EXCEPTION_MESSAGE, Providers.Constant.StringConstantProvider.OK_ACTION);
            }
            catch
            {
                // TODO Handle unespected exception!
            }
        }
    }
}