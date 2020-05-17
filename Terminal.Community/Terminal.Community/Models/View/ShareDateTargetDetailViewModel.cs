namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class ShareDateTargetDetailViewModel
    {
        /// <summary>
        /// Get or set the IP Address
        /// </summary>
        public string IPAddr { get; set; }
        /// <summary>
        /// Get or set the Hardware Address
        /// </summary>
        public string HWAddr { get; set; }
        /// <summary>
        /// Get or set the Device
        /// </summary>
        public string Device { get; set; }
        /// <summary>
        /// Get or set if client is reachable
        /// </summary>
        public bool IsReachable { get; set; }

        /// <summary>
        /// Default Ctor
        /// </summary>
        /// <param name="IPAddr">The IP Address</param>
        /// <param name="HWAddr">The Hardware Address</param>
        /// <param name="Device">The Device</param>
        /// <param name="IsReachable">If Reachable</param>
        public ShareDateTargetDetailViewModel(string IPAddr, string HWAddr, string Device, bool IsReachable)
        {
            this.IPAddr = IPAddr;
            this.HWAddr = HWAddr;
            this.Device = Device;
            this.IsReachable = IsReachable;
        }
    }
}
