[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers.DisplayOrientationManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers
{
    public class DisplayOrientationManager : Community.Managers.Facade.IDisplayOrientationManagerFacade
    {
        public void Lock(Xamarin.Essentials.DisplayOrientation orientation)
        {
            switch (orientation)
            {
                case Xamarin.Essentials.DisplayOrientation.Landscape:
                    Xamarin.Essentials.Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Locked | Android.Content.PM.ScreenOrientation.Landscape;

                    break;

                case Xamarin.Essentials.DisplayOrientation.Portrait:
                    Xamarin.Essentials.Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Locked | Android.Content.PM.ScreenOrientation.Portrait;

                    break;

                default:
                    Xamarin.Essentials.Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Sensor;

                    break;
            }
        }

        public void Unlock()
        {
            Xamarin.Essentials.Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Sensor;
        }
    }
}
