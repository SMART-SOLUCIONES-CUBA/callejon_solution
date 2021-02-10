namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Presenters.Control
{
    public interface IPresentableControl
    {
        System.EventHandler PresentationShowInEventHandler { get; set; }
        System.EventHandler PresentationEndedEventHandler { get; set; }
        System.TimeSpan RequiredExternalDelayTime { get; }
        bool RequireFullScreen { get; }
    }
}
