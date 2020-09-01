using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class VideoModelFactory
    {
        public static VideoModel BuildFor(Middleware.Media.Persistence.Entities.Video video, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var result = new VideoModel
            {
                Id = video.Id,
                Length = video.Length,
                CreateAt = video.CreateAt,
                AudioChannelOutput = video.AudioChannelOutput,
                AudioFormat = video.AudioFormat,
                ColorModel = video.ColorModel,
                Duration = video.Duration,
                Extension = video.Extension,
                Format = video.Format,
                Fps = video.Fps,
                FrameSize = video.FrameSize,
                About = video.About
            };

            if (video.Thumbnail != null)
            {
                result.Thumbnail = ThumbnailModelFactory.BuildFor(video.Thumbnail, mediaFileManager, cached);
            }

            return result;
        }
    }
}
