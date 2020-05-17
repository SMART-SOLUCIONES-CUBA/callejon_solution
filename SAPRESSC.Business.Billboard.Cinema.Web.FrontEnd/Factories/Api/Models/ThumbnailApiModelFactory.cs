using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class ThumbnailApiModelFactory
    {
        public static ThumbnailApiModel BuildFor(Middleware.Media.Persistence.Entities.Thumbnail thumbnail, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var result = new ThumbnailApiModel
            {
                Id = thumbnail.Id,
                ContentType = thumbnail.ContentType,
                Length = thumbnail.Length,
                About = thumbnail.About
            };

            if (cached)
            {
                result.CachedData = mediaFileManager.GetThumbnailFileContent(thumbnail.Id, Common.Constants.VIDEO_THUMBNAIL_EXTENSION);
            }

            return result;
        }
    }
}
