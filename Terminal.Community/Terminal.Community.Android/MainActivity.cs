
using Android.App;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid
{
    [Activity(Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation)]
    //[Activity(Icon = "@mipmap/icon", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.SensorPortrait)]
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

            Stormlion.ImageCropper.Droid.Platform.Init();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                Xamarin.Forms.DependencyService.Register<Community.Managers.Facade.IWiFiHostPotManagerFacade, Managers.OreoPlusWiFiHostPotManager>();
            }
            else
            {
                Xamarin.Forms.DependencyService.Register<Community.Managers.Facade.IWiFiHostPotManagerFacade, Managers.LegancyByReflectionWiFiHostPotManager>();
            }
            
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [Android.Runtime.GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            Stormlion.ImageCropper.Droid.Platform.OnActivityResult(requestCode, resultCode, data);
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