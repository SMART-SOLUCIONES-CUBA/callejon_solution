namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class FixScreenClientConfigurationDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<CPCC.Persistence.Entities.FixScreenClientConfiguration, System.Guid>
    {
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        public string Description { get; set; }

        public string AdvisesChainPlaceholder { get; set; }

        public string AdvisesChainWellcome { get; set; }

        public int DailyRoomBillboardItemMaxCount { get; set; }

        public int DailyRoomBillboardDisplayTime { get; set; }

        public int PresentationDetailDisplayTime { get; set; }

        public int EventDetailDisplayTime { get; set; }

        public int CinemaOverviewDisplayTime { get; set; }
        
        public int PlaylistSidebarItemsCount { get; set; }

        public int StartUpDelayTime { get; set; }

        public bool ShowPresentations { get; set; }

        public bool ShowEvents { get; set; }

        public void Import(Persistence.Entities.FixScreenClientConfiguration entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Description = entity.Description;
            AdvisesChainPlaceholder = entity.AdvisesChainPlaceholder;
            AdvisesChainWellcome = entity.AdvisesChainWellcome;
            DailyRoomBillboardItemMaxCount = entity.DailyRoomBillboardItemMaxCount;
            DailyRoomBillboardDisplayTime = entity.DailyRoomBillboardDisplayTime;
            PresentationDetailDisplayTime = entity.PresentationDetailDisplayTime;
            EventDetailDisplayTime = entity.EventDetailDisplayTime;
            CinemaOverviewDisplayTime = entity.CinemaOverviewDisplayTime;
            PlaylistSidebarItemsCount = entity.PlaylistSidebarItemsCount;
            StartUpDelayTime = entity.StartUpDelayTime;
            ShowPresentations = entity.ShowPresentations;
            ShowEvents = entity.ShowEvents;
        }
    }
}
