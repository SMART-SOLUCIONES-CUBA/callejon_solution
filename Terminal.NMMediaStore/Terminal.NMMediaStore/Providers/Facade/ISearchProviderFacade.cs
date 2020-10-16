namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface ISearchProviderFacade
    {
        System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel[]> GetLastSubscriptorSearchesAsync(string id);

        System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SearchModel[]> GetLastOtherSubscriptorsSearchesAsync(string id);
    }
}
