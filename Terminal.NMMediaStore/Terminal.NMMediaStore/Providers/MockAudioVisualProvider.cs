using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class MockAudioVisualProvider : Facade.IAudioVisualProviderFacade
    {
        public async System.Threading.Tasks.Task<PaginationModel<AudioVisualModel>> SearchAsync(SearchModel search, int page = 1, int limit = 20)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return new PaginationModel<AudioVisualModel>
            {
                HasPrevious = page != 1,
                HasNext = true,
                Limit = limit,
                Page = page,
                MaxPageCount = 10,
                List = BuildMockList()
            };
        }

        public async System.Threading.Tasks.Task<PaginationModel<AudioVisualModel>> GetPopularsAsync(int page = 1, int limit = 20)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return new PaginationModel<AudioVisualModel>
            {
                HasPrevious = false,
                HasNext = false,
                Limit = limit,
                Page = page,
                MaxPageCount = 1,
                List = BuildMockList()
            };
        }

        public async System.Threading.Tasks.Task<PaginationModel<AudioVisualModel>> GetLastsAsync(int page = 1, int limit = 20)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return new PaginationModel<AudioVisualModel>
            {
                HasPrevious = false,
                HasNext = false,
                Limit = limit,
                Page = page,
                MaxPageCount = 1,
                List = BuildMockList()
            };
        }

        public async System.Threading.Tasks.Task<PaginationModel<AudioVisualModel>> GetRecomendedsAsync(int page = 1, int limit = 20)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return new PaginationModel<AudioVisualModel>
            {
                HasPrevious = false,
                HasNext = false,
                Limit = limit,
                Page = page,
                MaxPageCount = 1,
                List = BuildMockList()
            };
        }

        private System.Collections.Generic.List<AudioVisualModel> BuildMockList()
        {
            return new System.Collections.Generic.List<AudioVisualModel>
            {
                new AudioVisualModel
                {
                    Id = 1,
                    Title = "After Earth",
                    Year = 2018,
                    Duration = new System.TimeSpan(2, 33, 0),
                    FileSize = 880640,
                    Resolution = "1920x1080",
                    Quality = SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.FHD,
                    Format = "MKV",
                    Language = new LanguageModel
                    {
                        Id = 1,
                        Name = "Español"
                    },
                    Country = new CountryModel { Id = 1, Name = "Estados Unidos" },
                    PosterImage = new ImageModel
                    {
                        Id = new System.Guid("361FC194-FD80-4EA4-B6A4-85EDF079980D"),
                        ContentType = "image/png"
                    },
                    PlacardImage = new ImageModel
                    {
                        Id = new System.Guid("0E6F1475-4227-4D3F-8979-304F1895275F"),
                        ContentType = "image/jpeg"
                    },
                    About = "Marina (Daniela Vega) una joven camarera aspirante a cantante y Orlando (Francisco Reyes), veinte años mayor, planean un futuro juntos. Tras una noche de fiesta, Marina lo lleva a urgencias, pero él muere al llegar al hospital.",
                    Genres = new GenreModel[]
                    {
                        new GenreModel
                        {
                            Id = 1,
                            Name = "Acción"
                        },
                        new GenreModel
                        {
                            Id = 2,
                            Name = "Aventura"
                        },
                        new GenreModel
                        {
                            Id = 5,
                            Name = "Ciencia Ficción"
                        },
                    }
                },
                new AudioVisualModel
                {
                    Id = 2,
                    Title = "SuperMan: Man in Steel",
                    Year = 2018,
                    Duration = new System.TimeSpan(2, 33, 0),
                    FileSize = 880640,
                    Resolution = "1920x1080",
                    Quality = SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.FHD,
                    Format = "MKV",
                    Language = new LanguageModel
                    {
                        Id = 2,
                        Name = "Inglés"
                    },
                    Country = new CountryModel { Id = 1, Name = "Estados Unidos" },
                    CoCountries = new CountryModel[]
                    {
                        new CountryModel { Id = 2, Name = "Reino Unido" }
                    },
                    PosterImage = new ImageModel
                    {
                        Id = new System.Guid("361FC194-FD80-4EA4-B6A4-85EDF079980D"),
                        ContentType = "image/png"
                    },
                    PlacardImage = new ImageModel
                    {
                        Id = new System.Guid("3FE40CB9-99FB-4DAA-AC29-E1E5C095B8A8"),
                        ContentType = "image/jpeg"
                    },
                    About = "Marina (Daniela Vega) una joven camarera aspirante a cantante y Orlando (Francisco Reyes), veinte años mayor, planean un futuro juntos. Tras una noche de fiesta, Marina lo lleva a urgencias, pero él muere al llegar al hospital.",
                    Genres = new GenreModel[]
                    {
                        new GenreModel
                        {
                            Id = 1,
                            Name = "Acción"
                        },
                        new GenreModel
                        {
                            Id = 2,
                            Name = "Aventura"
                        },
                        new GenreModel
                        {
                            Id = 5,
                            Name = "Ciencia Ficción"
                        },
                    }
                },
                new AudioVisualModel
                {
                    Id = 3,
                    Title = "Iron Man 3",
                    Year = 2018,
                    Duration = new System.TimeSpan(2, 33, 0),
                    FileSize = 880640,
                    Resolution = "1920x1080",
                    Quality = SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.FHD,
                    Format = "MKV",
                    Language = new LanguageModel
                    {
                        Id = 1,
                        Name = "Español"
                    },
                    Country = new CountryModel { Id = 1, Name = "Estados Unidos" },
                    PosterImage = new ImageModel
                    {
                        Id = new System.Guid("361FC194-FD80-4EA4-B6A4-85EDF079980D"),
                        ContentType = "image/png"
                    },
                    PlacardImage = new ImageModel
                    {
                        Id = new System.Guid("6ACE01F4-8F68-43CA-B6CC-A78EB7937AEC"),
                        ContentType = "image/jpeg"
                    },
                    About = "Marina (Daniela Vega) una joven camarera aspirante a cantante y Orlando (Francisco Reyes), veinte años mayor, planean un futuro juntos. Tras una noche de fiesta, Marina lo lleva a urgencias, pero él muere al llegar al hospital.",
                    Genres = new GenreModel[]
                    {
                        new GenreModel
                        {
                            Id = 1,
                            Name = "Acción"
                        },
                        new GenreModel
                        {
                            Id = 2,
                            Name = "Aventura"
                        },
                        new GenreModel
                        {
                            Id = 5,
                            Name = "Ciencia Ficción"
                        },
                    }
                },
                new AudioVisualModel
                {
                    Id = 4,
                    Title = "Batman: The Dark Knight Rises",
                    Year = 2018,
                    Duration = new System.TimeSpan(2, 33, 0),
                    FileSize = 880640,
                    Resolution = "1920x1080",
                    Quality = SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.FHD,
                    Format = "MKV",
                    Language = new LanguageModel
                    {
                        Id = 2,
                        Name = "Inglés"
                    },
                    HasSubtitle = true,
                    Country = new CountryModel { Id = 1, Name = "Estados Unidos" },
                    CoCountries = new CountryModel[]
                    {
                        new CountryModel { Id = 2, Name = "Reino Unido" }
                    },
                    PosterImage = new ImageModel
                    {
                        Id = new System.Guid("361FC194-FD80-4EA4-B6A4-85EDF079980D"),
                        ContentType = "image/png"
                    },
                    PlacardImage = new ImageModel
                    {
                        Id = new System.Guid("F2E828BB-60B3-40E5-B538-198CDDD28E6F"),
                        ContentType = "image/jpeg"
                    },
                    About = "Marina (Daniela Vega) una joven camarera aspirante a cantante y Orlando (Francisco Reyes), veinte años mayor, planean un futuro juntos. Tras una noche de fiesta, Marina lo lleva a urgencias, pero él muere al llegar al hospital.",
                    Genres = new GenreModel[]
                    {
                        new GenreModel
                        {
                            Id = 1,
                            Name = "Acción"
                        },
                        new GenreModel
                        {
                            Id = 2,
                            Name = "Aventura"
                        },
                        new GenreModel
                        {
                            Id = 5,
                            Name = "Ciencia Ficción"
                        },
                    }
                },
                new AudioVisualModel
                {
                    Id = 5,
                    Title = "Sin Escape",
                    Year = 2018,
                    Duration = new System.TimeSpan(2, 33, 0),
                    FileSize = 880640,
                    Resolution = "1920x1080",
                    Quality = SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Enumerations.AudioVisualQuality.FHD,
                    Format = "MKV",
                    Language = new LanguageModel
                    {
                        Id = 2,
                        Name = "Inglés"
                    },
                    Country = new CountryModel { Id = 1, Name = "Estados Unidos" },
                    PosterImage = new ImageModel
                    {
                        Id = new System.Guid("361FC194-FD80-4EA4-B6A4-85EDF079980D"),
                        ContentType = "image/png"
                    },
                    PlacardImage = new ImageModel
                    {
                        Id = new System.Guid("FD67E511-520E-4344-849E-B9C273A9A50E"),
                        ContentType = "image/jpeg"
                    },
                    About = "Marina (Daniela Vega) una joven camarera aspirante a cantante y Orlando (Francisco Reyes), veinte años mayor, planean un futuro juntos. Tras una noche de fiesta, Marina lo lleva a urgencias, pero él muere al llegar al hospital.",
                    Genres = new GenreModel[]
                    {
                        new GenreModel
                        {
                            Id = 1,
                            Name = "Acción"
                        },
                        new GenreModel
                        {
                            Id = 2,
                            Name = "Aventura"
                        },
                        new GenreModel
                        {
                            Id = 5,
                            Name = "Ciencia Ficción"
                        },
                    }
                }
            };
        }
    }
}
