namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Managers.Facade
{
    public interface IWiFiHostpotManagerFacade
    {
        void RegisterCallbackActions(System.Action<bool> checkPermissionsCallbackAction, System.Action<Models.View.WiFiHostpotDetailViewModel> startupRequestCallbackAction, System.Action<bool> shutdownRequestCallbackAction);

        void ReleaseCallbackActions();

        void CheckPermissions();

        void RequestStartup();

        void RequestShutdown();
    }
}
