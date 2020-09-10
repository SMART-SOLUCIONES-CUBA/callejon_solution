using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using System.Threading.Tasks;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemoteAudioVisualProvider : Facade.IAudioVisualProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediateca/audiovisuals";

        public Task<PaginationModel<AudioVisualModel>> GetLastsAsync(int page = 1, int limit = 20)
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<PaginationModel<AudioVisualModel>>(string.Format("{0}/lasts/sheet/{1}/limit/{2}", TARGET_API_BASE_QUERY, page, limit));
        }

        public Task<PaginationModel<AudioVisualModel>> GetPopularsAsync(int page = 1, int limit = 20)
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<PaginationModel<AudioVisualModel>>(string.Format("{0}/populars/sheet/{1}/limit/{2}", TARGET_API_BASE_QUERY, page, limit));
        }

        public Task<PaginationModel<AudioVisualModel>> GetRecomendedsAsync(int page = 1, int limit = 20)
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<PaginationModel<AudioVisualModel>>(string.Format("{0}/recomendeds/sheet/{1}/limit/{2}", TARGET_API_BASE_QUERY, page, limit));
        }

        public Task<PaginationModel<AudioVisualModel>> SearchAsync(SearchModel search, int page = 1, int limit = 20)
        {
            return Helpers.HttpClientHelper.Instance.PostAsync<SearchModel, PaginationModel<AudioVisualModel>>(string.Format("{0}/search/sheet/{1}/limit/{2}", TARGET_API_BASE_QUERY, page, limit), search);
        }
    }
}
