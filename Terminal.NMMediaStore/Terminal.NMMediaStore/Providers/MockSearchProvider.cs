using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class MockSearchProvider : Facade.ISearchProviderFacade
    {
        public async System.Threading.Tasks.Task<SearchModel[]> GetLastSubscriptorSearchesAsync(string id)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return BuildMockList();
        }

        public async System.Threading.Tasks.Task<SearchModel[]> GetLastOtherSubscriptorsSearchesAsync(string id)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return BuildMockList();
        }

        private SearchModel[] BuildMockList()
        {
            return new SearchModel[]
            {
                new SearchModel
                {
                    Id = 1,
                    TitleTemathic = "Vengadores"
                },
                new SearchModel
                {
                    Id = 2,
                    TitleTemathic = "Vengadores",
                    Year = 2009
                },
                new SearchModel
                {
                    Id = 3,
                    TitleTemathic = "Vengadores",
                    Tags = new TagModel[]
                    {
                        new TagModel { Id = 1, Name = "Periodo Especial" },
                        new TagModel { Id = 2, Name = "Extraterrestres" },
                        new TagModel { Id = 3, Name = "Medio Ambiente" }
                    }
                },
                new SearchModel
                {
                    Id = 4,
                    Country = new CountryModel { Id = 1, Name = "Francia", IsoCode = "fr" }
                },
                new SearchModel
                {
                    Id = 5,
                    TitleTemathic = "Vengadores"
                },
                new SearchModel
                {
                    Id = 6,
                    TitleTemathic = "Vengadores"
                },
                new SearchModel
                {
                    Id = 7,
                    TitleTemathic = "Vengadores"
                }
            };
        }
    }
}
