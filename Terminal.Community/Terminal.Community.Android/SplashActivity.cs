using Android.App;
using Android.Content.PM;
using System.Linq;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid
{
    [Activity(Theme = "@style/SplashTheme", ScreenOrientation = ScreenOrientation.SensorPortrait, MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();

            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    using (var dbContext = Community.Providers.DdContextProvider.GetDbContext())
                    {
                        var cinemas = dbContext.Cinemas.Count();
                    }
                }
                catch {}
            });

            var startupTask = new System.Threading.Tasks.Task(() => { StartupActionAsync(); });

            startupTask.Start();
        }

        private async void StartupActionAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(5));

            StartActivity(new Android.Content.Intent(Application.Context, typeof(MainActivity)));
        }

        public override void OnBackPressed() { }
    }
}