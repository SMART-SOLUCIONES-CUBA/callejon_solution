using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class GenresPage : XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.GenresPageModel>
    {
        public GenresPage() : base(new Models.Page.GenresPageModel { IsBusy = true })
        {
            InitializeComponent();

            IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IGenreProviderFacade>().GetAllAsync().ContinueWith(task =>
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

        private void GenreItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new FilterResultPage(new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                SubscriptionId = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.ISubscriptionManagerFacade>().GetAuthenticatedSubscritor().Subscription.Id,
                Genres = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[] { (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel }
            }));
        }
    }
}