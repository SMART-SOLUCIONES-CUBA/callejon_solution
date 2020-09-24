namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemoteProducerHouseProvider : Facade.IProducerHouseProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/producerhouses";

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel[]> GetAllAsync()
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel[]>(TARGET_API_BASE_QUERY);
        }

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel[]> GetRelevantsAsync()
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProducerHouseModel[]>(string.Format("{0}/relevant", TARGET_API_BASE_QUERY));
        }
    }
}
