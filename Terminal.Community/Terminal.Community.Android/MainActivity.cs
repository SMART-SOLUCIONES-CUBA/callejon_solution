
using Android.App;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid
{
    [Activity(Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            LeoJHarris.FormsPlugin.Droid.EnhancedEntryRenderer.Init(this);

            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                Xamarin.Forms.DependencyService.Register<Community.Managers.Facade.IWiFiHostpotManagerFacade, Managers.OreoPlusWiFiHostpotManager>();
            }
            else
            {
                Xamarin.Forms.DependencyService.Register<Community.Managers.Facade.IWiFiHostpotManagerFacade, Managers.LegancyByReflectionWiFiHostpotManager>();
            }

            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [Android.Runtime.GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}