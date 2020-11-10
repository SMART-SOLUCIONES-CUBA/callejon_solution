using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class PickGenresPage : XFToolKit.Mvvm.Pages.BindingContextedContentPage<Models.Page.PickGenresPageModel>
    {
        private System.Collections.Generic.List<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel> pickedGenres;
        private System.Action<System.Collections.Generic.List<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel>> successCompleteActionCallback;

        public PickGenresPage(System.Collections.Generic.List<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel> pickedGenres, System.Action<System.Collections.Generic.List<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel>> successCompleteActionCallback)
            : base(new Models.Page.PickGenresPageModel { IsBusy = true })
        {
            this.pickedGenres = pickedGenres;
            this.successCompleteActionCallback = successCompleteActionCallback;

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
                    BindingContextPageModel.Choises = task.Result.Select(item => new Models.View.GenreChoiseViewModel
                    {
                        Genre = item,
                        Choised = pickedGenres == null ? false : pickedGenres.Any(model => model.Id == item.Id)
                    }).ToArray();

                    BindingContextPageModel.IsBusy = false;

                    IsEnabled = true;
                });
            });
        }

        protected override bool OnBackButtonPressed()
        {
            CancelToolbarItem_Activated(this, System.EventArgs.Empty);

            return true;
        }

        private void CancelToolbarItem_Activated(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPopPageAsync();
        }

        private void ApplyToolbarItem_Activated(object sender, System.EventArgs eventArgs)
        {
            successCompleteActionCallback.Invoke(BindingContextPageModel.Choises.Where(item => item.Choised).Select(item => item.Genre).ToList());

            this.NavigationPopPageAsync();
        }
    }
}