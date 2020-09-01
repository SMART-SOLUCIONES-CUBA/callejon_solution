namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface IYearProviderFacade
    {
        System.Threading.Tasks.Task<int[]> GetRelevantAsync();

        System.Threading.Tasks.Task<int[]> GetAllAsync();
    }
}
