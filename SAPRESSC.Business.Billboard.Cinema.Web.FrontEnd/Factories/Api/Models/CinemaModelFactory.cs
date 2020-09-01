using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class CinemaModelFactory
    {
        public static CinemaModel BuildFor(Persistence.Entities.Cinema cinema, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var result = new CinemaModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                LocalizationLatitude = cinema.LocalizationLatitude,
                LocalizationLongitude = cinema.LocalizationLongitude,
                About = cinema.About,
                Rooms = cinema.Rooms.Select(room => new RoomModel { Id = room.Id, Name = room.Name }),
                Addresses = cinema.OrganizationPostalContactItemBridges.Select(bridge => new PostalContactModel { Id = bridge.ContactItem.Id, Label = bridge.ContactItem.Label, Address = bridge.ContactItem.Address }),
                Emails = cinema.OrganizationEmailContactItemBridges.Select(bridge => new EmailContactModel { Id = bridge.ContactItem.Id, Label = bridge.ContactItem.Label, EmailAddress = bridge.ContactItem.EmailAddress }),
                Phones = cinema.OrganizationPhoneContactItemBridges.Select(bridge => new PhoneContactModel { Id = bridge.ContactItem.Id, Label = bridge.ContactItem.Label, PhoneNumber = bridge.ContactItem.PhoneNumber }),
                Image = ImageModelFactory.BuildFor(cinema.Image, mediaFileManager, cached)
            };

            return result;
        }
    }
}
