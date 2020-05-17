using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    public class FixScreenClientConfiguration : Core.Persistence.Entities.IHabilitableEntity<System.Guid>
    {
        [Key]
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        public string Description { get; set; }

        [Required, MaxLength(255)]
        public string AdvisesChainPlaceholder { get; set; }

        [MaxLength(100)]
        public string AdvisesChainWellcome { get; set; }

        [Required, Range(2, 4)]
        public int DailyRoomBillboardItemMaxCount { get; set; }

        [Required, Range(1, 60)]
        public int DailyRoomBillboardDisplayTime { get; set; }

        [Required, Range(1, 300)]
        public int PresentationDetailDisplayTime { get; set; }

        [Required, Range(1, 300)]
        public int EventDetailDisplayTime { get; set; }

        [Required, Range(1, 300)]
        public int CinemaOverviewDisplayTime { get; set; }

        [Required, Range(0, 60)]
        public int StartUpDelayTime { get; set; }

        public bool ShowPresentations { get; set; }

        [Required]
        public int PlaylistSidebarItemsCount { get; set; }

        public bool ShowEvents { get; set; }
    }
}
