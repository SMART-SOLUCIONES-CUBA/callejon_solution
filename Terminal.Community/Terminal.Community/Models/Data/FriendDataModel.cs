namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.Data
{
    public class FriendDataModel
    {
        public System.Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsMale { get; set; }

        public byte[] ImageData { get; set; }

        public TerminalDataModel Terminal { get; set; }
    }
}