using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemotePersonalityProvider : Facade.IPersonalityProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/personalities";

        public System.Threading.Tasks.Task<PaginationModel<PersonalityModel>> GetPaginateAsync(int page = 1, int limit = 20)
        {
            return Common.Helpers.HttpClientHelper.Instance.GetAsync<PaginationModel<PersonalityModel>>(string.Format("{0}/page/{1}/limit/{2}", TARGET_API_BASE_QUERY, page, limit));
        }

        public System.Threading.Tasks.Task<PersonalityModel[]> GetRelevantActorsAsync()
        {
            return Common.Helpers.HttpClientHelper.Instance.GetAsync<PersonalityModel[]>(string.Format("{0}/relevant/actors/20", TARGET_API_BASE_QUERY));
        }

        public System.Threading.Tasks.Task<PersonalityModel[]> GetRelevantDirectorsAsync()
        {
            return Common.Helpers.HttpClientHelper.Instance.GetAsync<PersonalityModel[]>(string.Format("{0}/relevant/directors/20", TARGET_API_BASE_QUERY));
        }
    }
}
