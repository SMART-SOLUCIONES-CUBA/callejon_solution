using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class AudioVisualModel : ProductionModel
    {
        public string Slug { get; set; }

        public CategoryModel Category { get; set; }

        public ImageModel PlacardImage { get; set; }

        public string Format { get; set; }

        public AudioVisualQuality Quality { get; set; }

        public bool HasSubtitle { get; set; }

        public string Resolution { get; set; }

        public long FileSize { get; set; }
    }
}
