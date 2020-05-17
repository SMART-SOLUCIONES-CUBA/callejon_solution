using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class VideoApiModelFactory
    {
        public static VideoApiModel BuildFor(Middleware.Media.Persistence.Entities.Video video, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var result = new VideoApiModel
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
                result.Thumbnail = ThumbnailApiModelFactory.BuildFor(video.Thumbnail, mediaFileManager, cached);
            }

            return result;
        }
    }
}
