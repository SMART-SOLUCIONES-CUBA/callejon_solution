using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface IPersonalityProviderFacade
    {
        System.Threading.Tasks.Task<PersonalityModel[]> GetRelevantActorsAsync();

        System.Threading.Tasks.Task<PersonalityModel[]> GetRelevantDirectorsAsync();

        System.Threading.Tasks.Task<PaginationModel<PersonalityModel>> GetPaginateAsync(int page, int limit);
    }
}