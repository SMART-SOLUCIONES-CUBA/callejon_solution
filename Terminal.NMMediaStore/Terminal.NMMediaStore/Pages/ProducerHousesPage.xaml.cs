using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ProducerHousesPage : XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.ProducerHousesPageModel>
    {
        public ProducerHousesPage() : base(new Models.Page.ProducerHousesPageModel { IsBusy = true })
        {
            InitializeComponent();

            IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IProducerHouseProviderFacade>().GetAllAsync().ContinueWith(task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (task.Exception != null || task.IsCanceled)
                    {
                        (Xamarin.Forms.Application.Current as Helpers.Facade.IExceptionHandlingHelperFacade).HandleException(task.Exception);

                        this.NavigationPopPageAsync();
                    }
                    else
                    {
                        BindingContextPageModel.Items = task.Result;

                        BindingContextPageModel.IsBusy = false;

                        IsEnabled = true;
                    }
                });
            });
        }

        private void AlternativeBackButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPopPageAsync();
        }

        private void Item_Tapped(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                SubscriptionId = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>().GetAuthenticatedSubscrition().Id,
                ProducerHouse = (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel
            }));
        }
    }
}