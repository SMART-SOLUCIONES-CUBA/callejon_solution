using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface ISearchProviderFacade
    {
        System.Threading.Tasks.Task<SearchModel[]> GetLastSubscriptorSearchesAsync(string id);

        System.Threading.Tasks.Task<SearchModel[]> GetLastOtherSubscriptorsSearchesAsync();
    }
}
