using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface IAudioVisualProviderFacade
    {
        System.Threading.Tasks.Task<PaginationModel<AudioVisualModel>> SearchAsync(SearchModel search, int page = 1, int limit = 20);

        System.Threading.Tasks.Task<PaginationModel<AudioVisualModel>> GetPopularsAsync(int page = 1, int limit = 20);

        System.Threading.Tasks.Task<PaginationModel<AudioVisualModel>> GetLastsAsync(int page = 1, int limit = 20);

        System.Threading.Tasks.Task<PaginationModel<AudioVisualModel>> GetRecomendedsAsync(int page = 1, int limit = 20);
    }
}
