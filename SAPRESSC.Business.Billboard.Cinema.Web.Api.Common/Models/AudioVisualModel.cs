namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class AudioVisualModel : ProductionModel
    {
        public string Slug { get; set; }

        public ImageModel PlacardImage { get; set; }

        public string Format { get; set; }

        public Enumerations.AudioVisualQuality Quality { get; set; }

        public bool HasSubtitle { get; set; }

        public string Resolution { get; set; }

        public long FileSize { get; set; }
    }
}
