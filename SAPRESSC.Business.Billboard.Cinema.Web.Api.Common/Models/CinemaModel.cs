using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class CinemaModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double LocalizationLongitude { get; set; }

        public double LocalizationLatitude { get; set; }

        public string About { get; set; }

        public IEnumerable<EmailContactModel> Emails { get; set; }

        public IEnumerable<PhoneContactModel> Phones { get; set; }

        public IEnumerable<PostalContactModel> Addresses { get; set; }

        public IEnumerable<RoomModel> Rooms { get; set; }

        public ImageModel Image { get; set; }
    }
}
