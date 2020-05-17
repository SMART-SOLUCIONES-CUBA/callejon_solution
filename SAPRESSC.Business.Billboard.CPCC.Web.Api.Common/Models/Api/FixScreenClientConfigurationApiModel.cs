namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.Api
{
    public class FixScreenClientConfigurationApiModel
    {
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
    }
}
