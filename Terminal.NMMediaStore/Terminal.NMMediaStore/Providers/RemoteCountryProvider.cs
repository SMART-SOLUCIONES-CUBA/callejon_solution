namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemoteCountryProvider : Facade.ICountryProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/countries";

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CountryModel[]> GetAllAsync()
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CountryModel[]>(TARGET_API_BASE_QUERY);
        }
    }
}
