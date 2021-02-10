using Xamarin.Essentials;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Plugins.Essentials.Permission
{
    public class CalendarReadAndWritePermission : Permissions.BasePermission
    {
        public override async System.Threading.Tasks.Task<PermissionStatus> CheckStatusAsync()
        {
            var readPermissionStatus = await Permissions.CheckStatusAsync<Permissions.CalendarRead>();
            var writePermissionStatus = await Permissions.CheckStatusAsync<Permissions.CalendarWrite>();

            if (readPermissionStatus == PermissionStatus.Granted && writePermissionStatus == PermissionStatus.Granted)
            {
                return PermissionStatus.Granted;
            }

            return PermissionStatus.Unknown;
        }

        public override void EnsureDeclared()
        {
        }

        public override async System.Threading.Tasks.Task<PermissionStatus> RequestAsync()
        {
            var readPermissionStatus = await Permissions.RequestAsync<Permissions.CalendarRead>();
            var writePermissionStatus = await Permissions.RequestAsync<Permissions.CalendarWrite>();

            if (readPermissionStatus == PermissionStatus.Granted && writePermissionStatus == PermissionStatus.Granted)
            {
                return PermissionStatus.Granted;
            }

            return PermissionStatus.Unknown;
        }

        public override bool ShouldShowRationale()
        {
            // TODO Research what this do!!
            return true;
        }
    }
}
