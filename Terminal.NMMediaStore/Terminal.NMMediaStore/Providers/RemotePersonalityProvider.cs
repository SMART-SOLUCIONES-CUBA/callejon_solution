using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemotePersonalityProvider : Facade.IPersonalityProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediateca/personalities";

        public System.Threading.Tasks.Task<PaginationModel<PersonalityModel>> GetPaginateAsync(int page = 1, int limit = 20)
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<PaginationModel<PersonalityModel>>(string.Format("{0}/page/{1}/limit/{2}", TARGET_API_BASE_QUERY, page, limit));
        }

        public System.Threading.Tasks.Task<PersonalityModel[]> GetRelevantActorsAsync()
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<PersonalityModel[]>(string.Format("{0}/relevantactors", TARGET_API_BASE_QUERY));
        }

        public System.Threading.Tasks.Task<PersonalityModel[]> GetRelevantDirectorsAsync()
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<PersonalityModel[]>(string.Format("{0}/relevantdirectors", TARGET_API_BASE_QUERY));
        }
    }
}
