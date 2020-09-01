namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers
{
    public class MockFetchOrderManager : Facade.IFetchOrderManagerFacade
    {
        public async System.Threading.Tasks.Task<long> CountByProductionIdAsync(long productionId)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return 2089;
        }
    }
}
