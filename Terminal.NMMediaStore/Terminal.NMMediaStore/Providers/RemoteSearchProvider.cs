namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemoteSearchProvider : Facade.ISearchProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/searches";

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel[]> GetLastOtherSubscriptorsSearchesAsync(string id)
        {
            return Common.Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel[]>(string.Format("{0}/lastotherforsubscriptor/{1}", TARGET_API_BASE_QUERY, id));
        }

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel[]> GetLastSubscriptorSearchesAsync(string id)
        {
            return Common.Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel[]>(string.Format("{0}/lastforsubscriptor/{1}", TARGET_API_BASE_QUERY, id));
        }
    }
}
