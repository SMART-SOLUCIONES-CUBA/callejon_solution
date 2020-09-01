namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers
{
    public class MockProductionCheckoutManager : Facade.IProductionCheckoutManagerFacade
    {
        public async System.Threading.Tasks.Task AddAsync(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProductionCkeckoutModel model)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));
        }

        public async System.Threading.Tasks.Task<long> CountByProductionIdAsync(long productionId)
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return 4003;
        }
    }
}
