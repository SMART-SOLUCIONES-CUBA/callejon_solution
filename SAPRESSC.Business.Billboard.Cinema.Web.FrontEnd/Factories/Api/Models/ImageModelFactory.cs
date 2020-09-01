using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class ImageModelFactory
    {
        public static ImageModel BuildFor(Middleware.Media.Persistence.Entities.Image image, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var result = new ImageModel
            {
                Id = image.Id,
                ContentType = image.ContentType,
                Length = image.Length,
                CreateAt = image.CreateAt,
                Height = image.Height,
                Width = image.Width,
                About = image.About
            };

            if (cached)
            {
                result.CachedData = mediaFileManager.GetImageFileContent(image.Id);
            }

            if (image.Thumbnail != null)
            {
                result.Thumbnail = ThumbnailModelFactory.BuildFor(image.Thumbnail, mediaFileManager, cached);
            }

            return result;
        }
    }
}
