using System.IO;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Managers
{
    public class MediaFileManager : Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade
    {
        private const string MEDIA_ROOT_DIRECTORY_NAME = "media";
        private const string IMAGES_DIRECTORY_NAME = "images";
        private const string THUMBNAILS_DIRECTORY_NAME = "thumbnails";
        private const string VIDEOS_DIRECTORY_NAME = "videos";
        private const string TEMP_DIRECTORY_NAME = "temp";

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        public MediaFileManager(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env;
        }

        public void DeleteImageFile(System.Guid imageId)
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, IMAGES_DIRECTORY_NAME));

            File.Delete(targetDirectryInfo.PhysicalPath + Path.DirectorySeparatorChar + imageId);
        }

        public void DeleteThumbnailFile(System.Guid thumbnailId, string extension = "")
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, THUMBNAILS_DIRECTORY_NAME));

            File.Delete(targetDirectryInfo.PhysicalPath + Path.DirectorySeparatorChar + thumbnailId + extension);
        }

        public void DeleteVideoFile(System.Guid videoId, string extension)
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, VIDEOS_DIRECTORY_NAME));

            File.Delete(targetDirectryInfo.PhysicalPath + Path.DirectorySeparatorChar + videoId + extension);
        }

        public void DeleteVideoSegmentFileContent(System.Guid videoId, int segment)
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, VIDEOS_DIRECTORY_NAME, TEMP_DIRECTORY_NAME, videoId.ToString()));

            File.Delete(targetDirectryInfo.PhysicalPath + Path.DirectorySeparatorChar + segment);
        }

        public void EnsureThumbnailsDirectoryExist()
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, THUMBNAILS_DIRECTORY_NAME));

            if (!Directory.Exists(targetDirectryInfo.PhysicalPath))
            {
                Directory.CreateDirectory(targetDirectryInfo.PhysicalPath);
            }
        }

        public byte[] GetImageFileContent(System.Guid imageId)
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, IMAGES_DIRECTORY_NAME));

            byte[] result;

            using (var file = File.OpenRead(targetDirectryInfo.PhysicalPath + Path.DirectorySeparatorChar + imageId))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        public byte[] GetThumbnailFileContent(System.Guid thumbnailId, string extension = "")
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, THUMBNAILS_DIRECTORY_NAME));

            byte[] result;

            using (var file = File.OpenRead(targetDirectryInfo.PhysicalPath + Path.DirectorySeparatorChar + thumbnailId + extension))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        public string GetThumbnailFilePath(System.Guid thumbnailId, string extension = "")
        {
            return env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, THUMBNAILS_DIRECTORY_NAME, thumbnailId.ToString()) + extension).PhysicalPath;
        }

        public string GetVideoFilePath(System.Guid videoId, string extension)
        {
            return env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, VIDEOS_DIRECTORY_NAME, videoId.ToString())).PhysicalPath + extension;
        }

        public void MergeCahedVideoSegments(System.Guid videoId, byte[] fileContent, string extension)
        {
            var targetSegmentsDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, VIDEOS_DIRECTORY_NAME, TEMP_DIRECTORY_NAME, videoId.ToString()));
            var targetVideoDirectoryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, VIDEOS_DIRECTORY_NAME));

            var segments = Directory.GetFiles(targetSegmentsDirectryInfo.PhysicalPath).OrderBy(path => File.GetCreationTime(path)).ToArray();

            using (var targetVideoFileStream = File.Create(Path.Combine(targetVideoDirectoryInfo.PhysicalPath, string.Format("{0}{1}", videoId, extension))))
            {
                foreach (var segment in segments)
                {
                    using (var segmentFileStream = File.OpenRead(segment))
                    {
                        segmentFileStream.CopyTo(targetVideoFileStream);

                        segmentFileStream.Close();
                    }
                }

                targetVideoFileStream.Write(fileContent, 0, fileContent.Length);

                targetVideoFileStream.Close();
            }

            foreach (var segment in segments)
            {
                File.Delete(segment);
            }

            Directory.Delete(targetSegmentsDirectryInfo.PhysicalPath);
        }

        public void SaveImageFileContent(System.Guid imageId, byte[] fileContent)
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, IMAGES_DIRECTORY_NAME));

            if (!Directory.Exists(targetDirectryInfo.PhysicalPath))
            {
                Directory.CreateDirectory(targetDirectryInfo.PhysicalPath);
            }

            var filePath = targetDirectryInfo.PhysicalPath + Path.DirectorySeparatorChar + imageId;

            File.WriteAllBytes(filePath, fileContent);
        }

        public void SaveThumbnailFileContent(System.Guid thumbnailId, byte[] fileContent, string extension = "")
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, THUMBNAILS_DIRECTORY_NAME));

            if (!Directory.Exists(targetDirectryInfo.PhysicalPath))
            {
                Directory.CreateDirectory(targetDirectryInfo.PhysicalPath);
            }

            var filePath = targetDirectryInfo.PhysicalPath + Path.DirectorySeparatorChar + thumbnailId + extension;

            File.WriteAllBytes(filePath, fileContent);
        }

        public void SaveVideoFileContent(System.Guid videoId, byte[] fileContent, string extension)
        {
            var targetVideoDirectoryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, VIDEOS_DIRECTORY_NAME));

            if (!Directory.Exists(targetVideoDirectoryInfo.PhysicalPath))
            {
                Directory.CreateDirectory(targetVideoDirectoryInfo.PhysicalPath);
            }

            File.WriteAllBytes(Path.Combine(targetVideoDirectoryInfo.PhysicalPath, string.Format("{0}{1}", videoId, extension)), fileContent);
        }

        public void SaveVideoSegmentFileContent(System.Guid videoId, int segment, byte[] fileContent)
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, VIDEOS_DIRECTORY_NAME, TEMP_DIRECTORY_NAME, videoId.ToString()));

            if (!Directory.Exists(targetDirectryInfo.PhysicalPath))
            {
                Directory.CreateDirectory(targetDirectryInfo.PhysicalPath);
            }

            var filePath = targetDirectryInfo.PhysicalPath + Path.DirectorySeparatorChar + segment;

            File.WriteAllBytes(filePath, fileContent);
        }
    }
}
