namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class RemoteGenreProvider : Facade.IGenreProviderFacade
    {
        private const string TARGET_API_BASE_QUERY = "mediastore/genres";

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[]> GetAllAsync()
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[]>(TARGET_API_BASE_QUERY);
        }

        public System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[]> GetRelevantsAsync()
        {
            return Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.GenreModel[]>(string.Format("{0}/relevant", TARGET_API_BASE_QUERY));
        }
    }
}
