namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.FrontEnd.Factories.Api.Models
{
    public static class FixScreenClientConfigurationApiModelFactory
    {
        public static Web.Api.Common.Models.Api.FixScreenClientConfigurationApiModel BuildFor(Persistence.Entities.FixScreenClientConfiguration config)
        {
            return new Web.Api.Common.Models.Api.FixScreenClientConfigurationApiModel
            {
                AdvisesChainPlaceholder = config.AdvisesChainPlaceholder,
                AdvisesChainWellcome = config.AdvisesChainWellcome,
                CinemaOverviewDisplayTime = config.CinemaOverviewDisplayTime,
                DailyRoomBillboardDisplayTime = config.DailyRoomBillboardDisplayTime,
                DailyRoomBillboardItemMaxCount = config.DailyRoomBillboardItemMaxCount,
                EventDetailDisplayTime = config.EventDetailDisplayTime,
                ShowEvents = config.ShowEvents,
                PresentationDetailDisplayTime = config.PresentationDetailDisplayTime,
                ShowPresentations = config.ShowPresentations,
                StartUpDelayTime = config.StartUpDelayTime,
                PlaylistSidebarItemsCount = config.PlaylistSidebarItemsCount
            };
        }
    }
}
