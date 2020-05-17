namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Managers.Facade
{
    public interface IWiFiDataShareManager
    {
        string DeviceName { get; }

        void HandleApplicationResume();

        void HandleApplicationSleep();

        void StartSession(Presenters.Facade.IWiFiDataSharePresenterFacade presenter);

        void EndSession();

        void Connect(Models.View.FriendDeviceViewModel model);

        System.Threading.Tasks.Task TansferStoredDataAsync();
    }
}
