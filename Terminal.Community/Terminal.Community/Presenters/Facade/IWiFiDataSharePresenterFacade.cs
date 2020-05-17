namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Presenters.Facade
{
    public interface IWiFiDataSharePresenterFacade
    {
        void PresentStepCompleted(Enumerations.WiFiDataShareStep step, bool successfull, string detail = null);

        void PresentAvailableFriendDevices(Models.View.FriendDeviceViewModel[] friendDevices);
    }
}
