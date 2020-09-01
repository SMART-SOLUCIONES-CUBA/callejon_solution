namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers.Facade
{
    public interface IProductionCheckoutManagerFacade
    {
        System.Threading.Tasks.Task AddAsync(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.ProductionCkeckoutModel model);

        System.Threading.Tasks.Task<long> CountByProductionIdAsync(long productionId);
    }
}
