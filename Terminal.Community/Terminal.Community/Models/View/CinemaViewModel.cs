namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class CinemaViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double LocalizationLongitude { get; set; }

        public double LocalizationLatitude { get; set; }

        public RoomViewModel[] Rooms { get; set; }

        public EmailContactViewModel[] EmailContacts { get; set; }

        public PhoneContactViewModel[] PhoneContacts { get; set; }

        public System.Guid ImageId { get; set; }
    }
}
