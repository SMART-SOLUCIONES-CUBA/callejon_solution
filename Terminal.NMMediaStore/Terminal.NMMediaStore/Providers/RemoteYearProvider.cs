namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemoteYearProvider : Facade.IYearProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/years";

        public System.Threading.Tasks.Task<int[]> GetAllAsync()
        {
            return Common.Helpers.HttpClientHelper.Instance.GetAsync<int[]>(TARGET_API_BASE_QUERY);
        }

        public System.Threading.Tasks.Task<int[]> GetRelevantsAsync(int limit = 10)
        {
            return Common.Helpers.HttpClientHelper.Instance.GetAsync<int[]>(string.Format("{0}/relevant/{1}", TARGET_API_BASE_QUERY, limit));
        }
    }
}
