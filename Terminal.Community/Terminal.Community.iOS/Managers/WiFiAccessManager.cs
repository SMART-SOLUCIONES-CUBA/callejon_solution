using System.Linq;

[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Managers.WiFiAccessManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Managers
{
    public class WiFiAccessManager : Community.Managers.Facade.IWiFiAccessManagerFacade
    {
        private System.Action<int> connectCallbackAction;

        public WiFiAccessManager()
        {
        }

        public bool HasBuildinConfirmation => true;

        public bool IsPlatformSupported => UIKit.UIDevice.CurrentDevice.CheckSystemVersion(11, 0);

        public void RegisterCallbackActions(System.Action<int> connectCallbackAction)
        {
            this.connectCallbackAction = connectCallbackAction;
        }

        public void ReleaseCallbackActions()
        {
            connectCallbackAction = null;
        }

        public void Connect(string ssid, string password)
        {
            if (!IsPlatformSupported) throw new System.InvalidOperationException();

#pragma warning disable XI0002 // Notifies you from using newer Apple APIs when targeting an older OS version
            NetworkExtension.NEHotspotConfigurationManager.SharedManager.GetConfiguredSsids((configurations) =>
            {
                if (configurations == null) return;

                var targetConfiguration = configurations.FirstOrDefault(item => item == ssid);

                if (targetConfiguration != null)
                {
                    NetworkExtension.NEHotspotConfigurationManager.SharedManager.RemoveConfiguration(targetConfiguration);
                }
            });

            var configuration = new NetworkExtension.NEHotspotConfiguration(ssid, password, false)
            {
                JoinOnce = true
            };

            NetworkExtension.NEHotspotConfigurationManager.SharedManager.ApplyConfiguration(configuration, OnHotspotConfigurationManagerApplyConfigurationCallback);
#pragma warning restore XI0002 // Notifies you from using newer Apple APIs when targeting an older OS version
        }

        private void OnHotspotConfigurationManagerApplyConfigurationCallback(Foundation.NSError error)
        {
            connectCallbackAction?.Invoke(error == null ? 0 : (int)error.Code);
        }
    }
}