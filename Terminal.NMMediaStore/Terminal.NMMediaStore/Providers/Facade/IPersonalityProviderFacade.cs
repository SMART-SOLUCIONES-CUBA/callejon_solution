using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface IPersonalityProviderFacade
    {
        System.Threading.Tasks.Task<PersonalityModel[]> GetRelevantActorsAsync(int limit = 20);

        System.Threading.Tasks.Task<PersonalityModel[]> GetRelevantDirectorsAsync(int limit = 20);

        System.Threading.Tasks.Task<PaginationModel<PersonalityModel>> GetPaginateAsync(int page, int limit);
    }
}