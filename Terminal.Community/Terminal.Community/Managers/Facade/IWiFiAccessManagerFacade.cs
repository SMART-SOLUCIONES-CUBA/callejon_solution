namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Managers.Facade
{
    public interface IWiFiAccessManagerFacade
    {
        void RegisterCallbackActions(System.Action<int> connectCallbackAction);

        void ReleaseCallbackActions();

        bool HasBuilinConfirmation { get; }

        bool IsPlatformSupported { get; }

        void Connect(string ssid, string password);
    }
}
