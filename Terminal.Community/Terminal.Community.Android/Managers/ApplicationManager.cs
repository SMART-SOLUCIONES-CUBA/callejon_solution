[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers.ApplicationManager))]

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Droid.Managers
{
    public class ApplicationManager : Community.Managers.Facade.IApplicationManagerFacade
    {
        public void ClearDatabase()
        {
            var files = Android.App.Application.Context.FilesDir.ListFiles();

            foreach (var file in files)
            {
                var currentProfile = Community.Managers.ProfileManager.Instance.GetCurrentProfile();

                if (currentProfile != null && file.Name.EndsWith(currentProfile.Id.ToString())) continue;

                file.Delete();
            }
        }

        public void Close()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        public long GetDatabaseSize()
        {
            long result = 0;

            var files = Android.App.Application.Context.FilesDir.ListFiles();

            foreach (var file in files)
            {
                if (file.Name.EndsWith(Community.Managers.ProfileManager.Instance.GetCurrentProfile().Id.ToString())) continue;

                result += file.Length();
            }

            return result;
        }
    }
}