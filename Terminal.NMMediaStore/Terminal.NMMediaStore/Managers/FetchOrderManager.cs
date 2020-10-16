using System.Threading.Tasks;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers
{
    public class FetchOrderManager : Facade.IFetchOrderManagerFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/fetchorders";

        public Task<long> CountByProductionIdAsync(long productionId)
        {
            return Common.Helpers.HttpClientHelper.Instance.GetAsync<long>(string.Format("{0}/count/production/{1}", TARGET_API_BASE_QUERY, productionId));
        }

        public Task<long> Create(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.FetchOrderModel model)
        {
            return Common.Helpers.HttpClientHelper.Instance.PostAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.FetchOrderModel, long>(TARGET_API_BASE_QUERY, model);
        }
    }
}
