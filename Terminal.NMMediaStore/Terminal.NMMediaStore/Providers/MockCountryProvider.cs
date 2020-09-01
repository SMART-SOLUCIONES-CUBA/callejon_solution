namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class MockCountryProvider : Facade.ICountryProviderFacade
    {
        public async System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CountryModel[]> GetAllAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CountryModel[]
            {
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CountryModel
                {
                    Id = 1,
                    Name = "Cuba",
                    IsoCode = "cu"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CountryModel
                {
                    Id = 2,
                    Name = "España",
                    IsoCode = "es"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CountryModel
                {
                    Id = 3,
                    Name = "Francia",
                    IsoCode = "fr"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CountryModel
                {
                    Id = 4,
                    Name = "Reino Unido",
                    IsoCode = "uk"
                },
                new SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CountryModel
                {
                    Id = 5,
                    Name = "Estados Unidos",
                    IsoCode = "us"
                }
            };
        }
    }
}
