using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class BillboardPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public BillboardPage()
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.BillboardPageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var model = BindingContext as Models.Page.BillboardPageModel;

            if (!IsEnabled)
            {
                model.IsBusy = true;

                var stopwatch = new System.Diagnostics.Stopwatch();

                System.Threading.Tasks.Task.Run(() =>
                {
                    stopwatch.Start();

                    var list = new System.Collections.Generic.List<Models.View.CinemaViewModel>();

                    using (var dbContext = Providers.DdContextProvider.GetDbContext())
                    {
                        var cinemas = dbContext.Cinemas
                            .Include(entity => entity.Image)
                            .Include(entity => entity.Rooms)
                            .Include(entity => entity.OrganizationPhoneContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                            .Include(entity => entity.OrganizationEmailContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                            .Include(entity => entity.OrganizationPostalContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                            .ToArray();

                        foreach (var cinema in cinemas)
                        {
                            list.Add(new Models.View.CinemaViewModel
                            {
                                Id = cinema.Id,
                                Name = cinema.Name,
                                Address = cinema.OrganizationPostalContactItemBridges.First().ContactItem.Address,
                                LocalizationLatitude = cinema.LocalizationLatitude,
                                LocalizationLongitude = cinema.LocalizationLongitude,
                                Rooms = cinema.Rooms.Select(entity => new Models.View.RoomViewModel { Id = entity.Id, Name = entity.Name, CinemaName = cinema.Name }).ToArray(),
                                EmailContacts = cinema.OrganizationEmailContactItemBridges.Select(bridge => new Models.View.EmailContactViewModel { EmailAddress = bridge.ContactItem.EmailAddress, Label = bridge.ContactItem.Label }).ToArray(),
                                PhoneContacts = cinema.OrganizationPhoneContactItemBridges.Select(bridge => new Models.View.PhoneContactViewModel { PhoneNumber = bridge.ContactItem.PhoneNumber, Label = bridge.ContactItem.Label }).ToArray(),
                                ImageSource = Providers.ImageSourceProvider.Instance.Cached(cinema.Image.Id)
                            });
                        }
                    }

                    return list;
                }).ContinueWith(async task =>
                {
                    stopwatch.Stop();

                    var reasonableElapseTime = System.TimeSpan.FromSeconds(1);

                    if (stopwatch.Elapsed < reasonableElapseTime)
                    {
                        await System.Threading.Tasks.Task.Delay(reasonableElapseTime - stopwatch.Elapsed);
                    }

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        model.IsBusy = false;

                        IsEnabled = true;

                        if (task.Exception == null)
                        {
                            model.Cinemas = new DLToolkit.Forms.Controls.FlowObservableCollection<Models.View.CinemaViewModel>(task.Result);
                        }
                        else
                        {
                            System.Exception targetException = task.Exception;

                            while (targetException is System.AggregateException)
                            {
                                targetException = targetException.InnerException;
                            }

                            if (targetException is System.TypeLoadException)
                            {
                                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("Aviso", string.Format("Con la actualización de la aplicación, actualizar la base de datos es necesario para continuar.{0}{0}Disculpe las molestias que esto pueda ocasionar.", System.Environment.NewLine), "Aceptar", "Salir").ContinueWith((System.Threading.Tasks.Task<bool> alertTask) =>
                                    {
                                        if (alertTask.Result)
                                        {
                                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                                            {
                                                ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).PresentDatabaseStatusSection();
                                            });
                                        }
                                        else
                                        {
                                            Xamarin.Forms.DependencyService.Get<Managers.Facade.IApplicationManagerFacade>().Close();
                                        }
                                    });
                                });
                            }
                        }
                    });
                });
            }
        }

        private void DatabaseUpdateButton_Clicked(object sender, System.EventArgs e)
        {
            ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).PresentDatabaseStatusSection();
        }

        private void FlowListView_FlowItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            this.NavigationPushPageAsync(new CinemaDetailPage(e.Item as Models.View.CinemaViewModel));
        }
    }
}