[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers.WiFiAccessManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers
{
    public class WiFiAccessManager : Community.Managers.Facade.IWiFiAccessManagerFacade
    {
        public bool HasBuildinConfirmation => false;

        public bool IsPlatformSupported => false;

        public void Connect(string ssid, string password)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterCallbackActions(System.Action<int> connectCallbackAction)
        {
        }

        public void ReleaseCallbackActions()
        {
        }
    }
}
