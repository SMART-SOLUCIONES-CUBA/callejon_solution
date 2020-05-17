namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api
{
    public class ThumbnailApiModel
    {
        public System.Guid Id { get; set; }

        public string ContentType { get; set; }

        public long Length { get; set; }

        public string About { get; set; }

        public byte[] CachedData { get; set; }
    }
}
