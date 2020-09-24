namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemoteAdvertisementProvider : Facade.IAdvertisementProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/advertisements";

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AdvertisementModel[]> GetAllAsync()
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.AdvertisementModel[]>(TARGET_API_BASE_QUERY);
        }
    }
}
