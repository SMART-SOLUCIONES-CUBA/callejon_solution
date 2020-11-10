namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Facade
{
    public interface IYearProviderFacade
    {
        System.Threading.Tasks.Task<int[]> GetRelevantsAsync(int limit = 10);

        System.Threading.Tasks.Task<int[]> GetAllAsync();
    }
}
