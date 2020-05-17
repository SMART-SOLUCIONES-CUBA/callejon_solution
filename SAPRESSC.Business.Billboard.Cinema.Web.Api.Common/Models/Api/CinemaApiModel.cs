using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api
{
    public class CinemaApiModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double LocalizationLongitude { get; set; }

        public double LocalizationLatitude { get; set; }

        public string About { get; set; }

        public IEnumerable<EmailContactApiModel> Emails { get; set; }

        public IEnumerable<PhoneContactApiModel> Phones { get; set; }

        public IEnumerable<PostalContactApiModel> Addresses { get; set; }

        public IEnumerable<RoomApiModel> Rooms { get; set; }

        public ImageApiModel Image { get; set; }
    }
}
