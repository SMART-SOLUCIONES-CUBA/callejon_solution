namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemoteCategoryProvider : Facade.ICategoryProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/categories";

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[]> GetAllAsync()
        {
            return Common.Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[]>(TARGET_API_BASE_QUERY);
        }

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[]> GetByParentAsync(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel parent)
        {
            if (parent != null)
            {
                return Common.Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[]>(string.Format("{0}/children/{1}", TARGET_API_BASE_QUERY, parent.Id));
            }

            return Common.Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CategoryModel[]>(string.Format("{0}/children", TARGET_API_BASE_QUERY));
        }
    }
}
