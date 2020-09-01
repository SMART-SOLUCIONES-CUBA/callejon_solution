namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers.Facade
{
    public interface ICommentManagerFacade
    {
        System.Threading.Tasks.Task<long> CountByProductionIdAsync(long productionId);
    }
}
