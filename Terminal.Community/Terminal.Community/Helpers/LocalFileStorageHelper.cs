namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Helpers
{
    internal static class LocalFileStorageHelper
    {
        internal static string GetDatabaseFilePath()
        {
            return System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "cpcc_pcm_billboard.db");
        }

        internal static string GetFilePath(string fileName)
        {
            return System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), fileName);
        }

        internal static string GetImageFilePath(System.Guid id)
        {
            return GetFilePath(id.ToString());
        }

        internal static string GetResponsiveImageFilePath(System.Guid id, int width)
        {
            return GetFilePath(string.Format("{0}-{1}", id, width));
        }

        internal static void DeleteLocalFiles()
        {
            var files = System.IO.Directory.GetFiles(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));

            foreach (var file in files)
            {
                var currentProfile = Managers.ProfileManager.Instance.GetCurrentProfile();

                if (currentProfile != null && file.EndsWith(currentProfile.Id.ToString())) continue;

                System.IO.File.Delete(file);
            }
        }

        internal static long GetLocalFileDataSize()
        {
            long result = 0;

            var files = System.IO.Directory.GetFiles(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));

            foreach (var file in files)
            {
                if (file.EndsWith(Managers.ProfileManager.Instance.GetCurrentProfile().Id.ToString())) continue;

                var fileInfo = new System.IO.FileInfo(file);

                result += fileInfo.Length;
            }

            return result;
        }
    }
}
