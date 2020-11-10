using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Extensions;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class FilterResultPage : Base.FetchOrderAudioVisualPresentBaseContentPage
    {
        public FilterResultPage(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel model)
        {
            InitializeComponent();

            BindingContext = new Models.Page.FilterResultPageModel
            {
                Search = model,
                IsSearchSummaryExpanded = false
            };

            IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            LoadNextPageSearchResults();

            IsEnabled = true;
        }

        private Models.Page.FilterResultPageModel BindingContextModel => BindingContext as Models.Page.FilterResultPageModel;

        private void AlternativeBackButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPopPageAsync();
        }

        private void AudioVisualItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            var model = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.AudioVisualViewModel;

            this.NavigationPushPageAsync(new AudioVisualDetailPage(model, this));
        }

        private void LoadNextPageButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            LoadNextPageSearchResults();
        }

        private void CategoryItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            BindingContextModel.Search = new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel
            {
                SubscriptionId = BindingContextModel.Search.SubscriptionId,
                Category = (sender as Xamarin.Forms.BindableObject).BindingContext as SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel
            };

            BindingContextModel.SubCategories = null;
            BindingContextModel.Results = null;

            LoadNextPageSearchResults();
        }

        private void AudioVisualItemFetchMarkToogleButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            try
            {
                var model = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.AudioVisualViewModel;

                if (model.IsMarkedForFetch)
                {
                    Xamarin.Forms.Application.Current.RemoveAudioVisualForFetch(model.AudioVisual);
                }
                else
                {
                    Xamarin.Forms.Application.Current.AddAudioVisualForFetch(model.AudioVisual);
                }

                OnAudioViualFetchMarkToogle(model.AudioVisual);
            }
            catch (Exceptions.UnspecifiedStorageDeviceException)
            {
                DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, Providers.Constant.StringConstantProvider.UNSPECIFIED_STORAGE_DEVICE_EXCEPTION_MESSAGE, Providers.Constant.StringConstantProvider.OK_ACTION);
            }
            catch (Exceptions.InsufficientStorageSpaceException)
            {
                DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, Providers.Constant.StringConstantProvider.INSUFFICIENT_STORAGE_SPACE_EXCEPTION_MESSAGE, Providers.Constant.StringConstantProvider.OK_ACTION);
            }
            catch (Exceptions.LockedAudioVisualForFetchListException)
            {
                DisplayAlert(Providers.Constant.StringConstantProvider.ADVICE_ALERT_DIALOG_TITLE, Providers.Constant.StringConstantProvider.LOCKED_AUDIOVISUAL_FOR_FETCH_LIST_EXCEPTION_MESSAGE, Providers.Constant.StringConstantProvider.OK_ACTION);
            }
            catch
            {
                // TODO Handle unespected exception!
            }
        }

        public override void OnAudioViualFetchMarkToogle(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AudioVisualModel target)
        {
            var models = BindingContextModel.Results.Where(item => item.AudioVisual.Id == target.Id).ToArray();

            foreach (var item in models)
            {
                item.IsMarkedForFetch = !item.IsMarkedForFetch;
            }
        }

        private void LoadNextPageSearchResults()
        {
            BindingContextModel.IsBusy = true;
            BindingContextModel.PaginationHasNext = false;

            System.Threading.Tasks.Task.Run(async () =>
            {
                var audioVisualProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.IAudioVisualProviderFacade>();
                
                var nextPage = BindingContextModel.PaginationCurrentPage + 1;

                SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[] subCategories = null;

                if (BindingContextModel.Search.Category != null)
                {
                    var categoryProvider = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Providers.Facade.ICategoryProviderFacade>();

                    subCategories = await categoryProvider.GetByParentAsync(BindingContextModel.Search.Category);

                    if (subCategories != null && subCategories.Length <= 0)
                    {
                        subCategories = null;
                    }
                }

                var result = await audioVisualProvider.SearchAsync(BindingContextModel.Search, nextPage);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (BindingContextModel == null) return;

                    BindingContextModel.PaginationCurrentPage = result.Page;

                    if (result.SerializedContext != null)
                    {
                        BindingContextModel.Search.Id = int.Parse(result.SerializedContext);
                    }

                    var fetchOrderManager = XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Managers.Facade.IFetchOrderManagerFacade>();

                    if (BindingContextModel.Results == null)
                    {
                        BindingContextModel.Results = new System.Collections.ObjectModel.ObservableCollection<Models.View.AudioVisualViewModel>();
                    }

                    if (result.List.Count > 0)
                    {
                        foreach (var item in result.List)
                        {
                            BindingContextModel.Results.Add(new Models.View.AudioVisualViewModel
                            {
                                AudioVisual = item,
                                IsMarkedForFetch = Xamarin.Forms.Application.Current.HasAudioVisualForFetch(item)
                            });
                        }

                        BindingContextModel.PaginationHasNext = result.HasNext;
                    }
                    else
                    {
                        BindingContextModel.PaginationHasNext = false;
                    }

                    BindingContextModel.SubCategories = subCategories;

                    BindingContextModel.IsBusy = false;
                });
            }).ContinueWith(filterTask =>
            {
                if (filterTask.Exception != null || filterTask.IsCanceled)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        XFToolKit.IoC.TinyIoCContainer.Current.Resolve<Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(filterTask.Exception);
                    });
                }
            });
        }
    }
}