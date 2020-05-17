namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class ShareDataServerViewModel
    {
        public Enumerations.WiFiDataShareTerminalState Status { get; set; }

        public string SecondaryType { get; set; }

        public string PrimaryType { get; set; }

        public string DeviceName { get; set; }

        public bool IsGroupOwner { get; set; }

        public bool IsServiceDiscoveryCapable { get; set; }

        public string DeviceAddress { get; set; }
    }
}
