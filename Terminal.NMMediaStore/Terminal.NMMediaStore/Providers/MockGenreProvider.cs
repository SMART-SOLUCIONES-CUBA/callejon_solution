namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class MockGenreProvider : Facade.IGenreProviderFacade
    {
        public async System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[]> GetAllAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return BuildMockList();
        }

        public async System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[]> GetRelevantsAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return BuildMockList();
        }

        private SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[] BuildMockList()
        {
            return new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[]
            {
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel
                {
                    Id = 1,
                    Name = "Acción"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel
                {
                    Id = 2,
                    Name = "Aventura"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel
                {
                    Id = 3,
                    Name = "Drama"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel
                {
                    Id = 4,
                    Name = "Ficción"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel
                {
                    Id = 5,
                    Name = "Ciencia Ficción"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel
                {
                    Id = 6,
                    Name = "Comedia"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel
                {
                    Id = 7,
                    Name = "Terror"
                }
            };
        }
    }
}
