using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class RoomBillboardPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public RoomBillboardPage(RoomViewModel model)
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.RoomBillboardPageModel
            {
                Room = model
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var model = BindingContext as Models.Page.RoomBillboardPageModel;

            if (!IsEnabled)
            {
                model.IsBusy = true;

                var stopwatch = new System.Diagnostics.Stopwatch();

                System.Threading.Tasks.Task.Run(() =>
                {
                    stopwatch.Start();

                    var list = new System.Collections.Generic.List<RoomDayBillboardViewModel>();

                    using (var dbContext = Providers.DdContextProvider.GetDbContext())
                    {
                        var presentations = dbContext.Presentations
                            .Include(entity => entity.Production).ThenInclude(entity => entity.ProductionGenreBridges).ThenInclude(bridge => bridge.Genre)
                            .Include(entity => entity.Production).ThenInclude(entity => entity.ProducerHouseProductionBridges).ThenInclude(bridge => bridge.ProducerHouse)
                            .Include(entity => entity.Production).ThenInclude(entity => entity.Language)
                            .Include(entity => entity.Production).ThenInclude(entity => entity.Country)
                            .Include(entity => entity.Production).ThenInclude(entity => entity.AwardWinnings).ThenInclude(relation => relation.Award)
                            .Include(entity => entity.Production).ThenInclude(entity => entity.Participations).ThenInclude(relation => relation.Personality).ThenInclude(entity => entity.Image)
                            .Include(entity => entity.Production).ThenInclude(entity => entity.PosterImage)
                            .Include(entity => entity.Production).ThenInclude(entity => entity.Clasification)
                            .Include(entity => entity.Production).ThenInclude(entity => entity.CountryCoProductionBridges).ThenInclude(bridge => bridge.Country)
                            .Where(entity => entity.Room.Id == model.Room.Id)
                            .OrderBy(entity => entity.Date);

                        RoomDayBillboardViewModel currentRoomDayBillboardViewModel = null;

                        foreach (var presentation in presentations)
                        {
                            if (currentRoomDayBillboardViewModel == null || currentRoomDayBillboardViewModel.Date.Day != presentation.Date.Date.Day)
                            {
                                if (currentRoomDayBillboardViewModel != null)
                                {
                                    list.Add(currentRoomDayBillboardViewModel);
                                }

                                currentRoomDayBillboardViewModel = new RoomDayBillboardViewModel { Date = presentation.Date.Date };
                            }

                            var countryNames = new System.Collections.Generic.List<string> { presentation.Production.Country.Name };

                            countryNames.AddRange(presentation.Production.CountryCoProductionBridges.Select(bridge => bridge.Country.Name));

                            currentRoomDayBillboardViewModel.Add(new PresentationViewModel
                            {
                                Id = presentation.Id,
                                PresentationDate = presentation.Date,
                                CinemaName = model.Room.CinemaName,
                                RoomName = model.Room.Name,
                                Production = new ProductionViewModel
                                {
                                    Id = presentation.Production.Id,
                                    Title = presentation.Production.LocalizedTitle ?? presentation.Production.Title,
                                    Duration = System.TimeSpan.FromMinutes(presentation.Production.Duration),
                                    Year = presentation.Production.Year,
                                    Clasification = presentation.Production.Clasification.Name,
                                    MainCountry = presentation.Production.Country.Name,
                                    CountriesJoin = string.Join(", ", countryNames.ToArray()),
                                    Language = presentation.Production.Language.Name,
                                    Rate = presentation.Production.Rate,
                                    About = presentation.Production.About,
                                    Argument = presentation.Production.Argument,
                                    Genres = presentation.Production.GetGenres().Select(entity => new GenreViewModel { Id = entity.Id, Name = entity.Name }).ToArray(),
                                    AwardWinnings = presentation.Production.AwardWinnings.Select(entity => string.Format("{0} Premios {1} {2}, {3}", entity.Type == SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations.AwardWinType.Winner ? "Ganador" : "Nominado", entity.Award.Name, entity.Edition, entity.Category)).ToArray(),
                                    ProducerHouses = presentation.Production.GetProducerHouses().Select(entity => new ProducerHouseViewModel { Id = entity.Id, Name = entity.Name }).ToArray(),
                                    PosterImageId = presentation.Production.PosterImage.Id,
                                    Directors = presentation.Production.Participations.Where(p => p.Role == SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations.ProductionRole.Direction).Select(p => new PersonalityViewModel
                                    {
                                        Id = p.Personality.Id,
                                        BornDate = p.Personality.BornDate,
                                        Name = p.Personality.Name,
                                        LastName = p.Personality.LastName,
                                        IsMale = p.Personality.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male,
                                        About = p.Personality.About,
                                        ImageId = p.Personality.Image?.Id ?? System.Guid.Empty
                                    }).ToArray(),
                                    Interpreters = presentation.Production.Participations.Where(p => p.Role == SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations.ProductionRole.Interpretation).Select(p => new PersonalityViewModel
                                    {
                                        Id = p.Personality.Id,
                                        BornDate = p.Personality.BornDate,
                                        Name = p.Personality.Name,
                                        LastName = p.Personality.LastName,
                                        IsMale = p.Personality.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male,
                                        About = p.Personality.About,
                                        ImageId = p.Personality.Image?.Id ?? System.Guid.Empty
                                    }).ToArray(),
                                    Productors = presentation.Production.Participations.Where(p => p.Role == SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations.ProductionRole.Production).Select(p => new PersonalityViewModel
                                    {
                                        Id = p.Personality.Id,
                                        BornDate = p.Personality.BornDate,
                                        Name = p.Personality.Name,
                                        LastName = p.Personality.LastName,
                                        IsMale = p.Personality.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male,
                                        About = p.Personality.About,
                                        ImageId = p.Personality.Image?.Id ?? System.Guid.Empty
                                    }).ToArray(),
                                    Editors = presentation.Production.Participations.Where(p => p.Role == SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations.ProductionRole.Edition).Select(p => new PersonalityViewModel
                                    {
                                        Id = p.Personality.Id,
                                        BornDate = p.Personality.BornDate,
                                        Name = p.Personality.Name,
                                        LastName = p.Personality.LastName,
                                        IsMale = p.Personality.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male,
                                        About = p.Personality.About,
                                        ImageId = p.Personality.Image?.Id ?? System.Guid.Empty
                                    }).ToArray(),
                                    Photographers = presentation.Production.Participations.Where(p => p.Role == SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations.ProductionRole.Photography).Select(p => new PersonalityViewModel
                                    {
                                        Id = p.Personality.Id,
                                        BornDate = p.Personality.BornDate,
                                        Name = p.Personality.Name,
                                        LastName = p.Personality.LastName,
                                        IsMale = p.Personality.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male,
                                        About = p.Personality.About,
                                        ImageId = p.Personality.Image?.Id ?? System.Guid.Empty
                                    }).ToArray(),
                                    ArtisticManagers = presentation.Production.Participations.Where(p => p.Role == SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations.ProductionRole.ArtisticManagement).Select(p => new PersonalityViewModel
                                    {
                                        Id = p.Personality.Id,
                                        BornDate = p.Personality.BornDate,
                                        Name = p.Personality.Name,
                                        LastName = p.Personality.LastName,
                                        IsMale = p.Personality.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male,
                                        About = p.Personality.About,
                                        ImageId = p.Personality.Image?.Id ?? System.Guid.Empty
                                    }).ToArray(),
                                    Screenplayers = presentation.Production.Participations.Where(p => p.Role == SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations.ProductionRole.Screenplay).Select(p => new PersonalityViewModel
                                    {
                                        Id = p.Personality.Id,
                                        BornDate = p.Personality.BornDate,
                                        Name = p.Personality.Name,
                                        LastName = p.Personality.LastName,
                                        IsMale = p.Personality.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male,
                                        About = p.Personality.About,
                                        ImageId = p.Personality.Image?.Id ?? System.Guid.Empty
                                    }).ToArray(),
                                    Sounders = presentation.Production.Participations.Where(p => p.Role == SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations.ProductionRole.Sound).Select(p => new PersonalityViewModel
                                    {
                                        Id = p.Personality.Id,
                                        BornDate = p.Personality.BornDate,
                                        Name = p.Personality.Name,
                                        LastName = p.Personality.LastName,
                                        IsMale = p.Personality.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male,
                                        About = p.Personality.About,
                                        ImageId = p.Personality.Image?.Id ?? System.Guid.Empty
                                    }).ToArray()
                                }
                            });
                        }

                        if (currentRoomDayBillboardViewModel != null)
                        {
                            list.Add(currentRoomDayBillboardViewModel);
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
                            model.DayPresentations = new System.Collections.ObjectModel.ObservableCollection<RoomDayBillboardViewModel>(task.Result);
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

        private void PresentationOverviewFrame_Tapped(object sender, System.EventArgs e)
        {
            this.NavigationPushPageAsync(new PresentationDetailPage((sender as Xamarin.Forms.BindableObject).BindingContext as PresentationViewModel));
        }
    }
}