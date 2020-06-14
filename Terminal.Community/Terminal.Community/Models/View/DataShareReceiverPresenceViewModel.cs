namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class DataShareReceiverPresenceViewModel
    {
        public System.DateTime At { get; set; }

        public System.Net.IPAddress IPAddress { get; set; }

        public Data.FriendDataModel Friend { get; set; }

        public int GridColumn { get; set; }

        public int GridRow { get; set; }
    }
}
