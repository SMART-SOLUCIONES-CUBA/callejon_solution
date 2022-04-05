namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class ProductionAudioVisual : Core.Persistence.Entities.IEntity<long>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        public Production Production { get; set; }

        public long ProductionId { get; set; }

        public string Format { get; set; }

        public Enumerations.AudioVisualQuality Quality { get; set; }

        public bool HasSubtitle { get; set; }

        public string Resolution { get; set; }

        public long FileSize { get; set; }
    }
}
