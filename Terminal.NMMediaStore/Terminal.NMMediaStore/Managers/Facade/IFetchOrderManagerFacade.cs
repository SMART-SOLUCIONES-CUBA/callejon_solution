using SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Models;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers.Facade
{
    public interface IFetchOrderManagerFacade
    {
        System.Threading.Tasks.Task<long> CountByProductionIdAsync(long productionId);
    }
}
