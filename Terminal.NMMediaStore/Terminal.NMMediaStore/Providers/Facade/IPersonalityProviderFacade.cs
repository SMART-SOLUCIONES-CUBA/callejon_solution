namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface IPersonalityProviderFacade
    {
        System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.PersonalityModel[]> GetRelevantActorsAsync();

        System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.PersonalityModel[]> GetRelevantDirectorsAsync();

        System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.PersonalityModel[]> GetAllAsync();
    }
}
