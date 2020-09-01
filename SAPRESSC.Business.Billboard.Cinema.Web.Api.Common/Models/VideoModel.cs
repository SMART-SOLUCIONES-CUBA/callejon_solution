namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class VideoModel
    {
        public System.Guid Id { get; set; }

        public System.DateTime CreateAt { get; set; }

        public string Extension { get; set; }

        public long Length { get; set; }

        public string About { get; set; }

        public System.TimeSpan Duration { get; set; }

        public string Format { get; set; }

        public string FrameSize { get; set; }

        public string ColorModel { get; set; }

        public double Fps { get; set; }

        public string AudioFormat { get; set; }

        public string AudioChannelOutput { get; set; }

        public ThumbnailModel Thumbnail { get; set; }
    }
}
