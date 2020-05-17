namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Managers.Facade
{
    public interface IWiFiHostPotManagerFacade
    {
        void RegisterCallbackActions(System.Action<bool> checkPermissionsCallbackAction, System.Action<Models.View.WiFiHostPotDetailViewModel> startupRequestCallbackAction, System.Action<bool> shutdownRequestCallbackAction);

        void CheckPermissions();

        void RequestStartup();

        void RequestShutdown();
    }
}
