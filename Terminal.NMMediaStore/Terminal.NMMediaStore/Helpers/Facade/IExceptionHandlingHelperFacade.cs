namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Helpers.Facade
{
    public interface IExceptionHandlingHelperFacade
    {
        void HandleException(System.Exception exception, System.Action callbackAction = null);
    }
}
