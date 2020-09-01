namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface ICountryProviderFacade
    {
        System.Threading.Tasks.Task<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.CountryModel[]> GetAllAsync();
    }
}
