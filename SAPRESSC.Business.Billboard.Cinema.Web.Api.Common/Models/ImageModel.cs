namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class ImageModel
    {
        public System.Guid Id { get; set; }

        public System.DateTime CreateAt { get; set; }

        public string ContentType { get; set; }

        public long Length { get; set; }
        
        public string About { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public ThumbnailModel Thumbnail { get; set; }

        public byte[] CachedData { get; set; }
    }
}
