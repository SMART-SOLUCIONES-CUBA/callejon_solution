namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers.Facade
{
    public interface IFetchOrderManagerFacade
    {
        System.Threading.Tasks.Task<long> CountByProductionIdAsync(long productionId);

        System.Threading.Tasks.Task<long> Create(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.FetchOrderModel model);
    }
}