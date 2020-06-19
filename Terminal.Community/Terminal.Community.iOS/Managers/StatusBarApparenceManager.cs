[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Managers.StatusBarApparenceManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.iOS.Managers
{
    public class StatusBarApparenceManager : Community.Managers.Facade.IStatusBarApparenceManagerFacade
    {
        public void RequireDarkTextApparence()
        {
            UIKit.UIApplication.SharedApplication.SetStatusBarStyle(UIKit.UIStatusBarStyle.DarkContent, true);

            GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
        }

        public void RequireLigthTextApparence()
        {
            UIKit.UIApplication.SharedApplication.SetStatusBarStyle(UIKit.UIStatusBarStyle.LightContent, true);

            GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
        }

        private UIKit.UIViewController GetCurrentViewController()
        {
            var target = UIKit.UIApplication.SharedApplication.KeyWindow.RootViewController;

            while (target.PresentedViewController != null)
            {
                target = target.PresentedViewController;
            }

            return target;
        }
    }
}
