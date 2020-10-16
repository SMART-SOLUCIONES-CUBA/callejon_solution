namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemoteTagProvider : Facade.ITagProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/tags";

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel[]> GetAllAsync()
        {
            return Common.Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.TagModel[]>(TARGET_API_BASE_QUERY);
        }
    }
}
