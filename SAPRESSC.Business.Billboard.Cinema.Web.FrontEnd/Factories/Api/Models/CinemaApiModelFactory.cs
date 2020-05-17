using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class CinemaApiModelFactory
    {
        public static CinemaApiModel BuildFor(Persistence.Entities.Cinema cinema, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, bool cached)
        {
            var result = new CinemaApiModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                LocalizationLatitude = cinema.LocalizationLatitude,
                LocalizationLongitude = cinema.LocalizationLongitude,
                About = cinema.About,
                Rooms = cinema.Rooms.Select(room => new RoomApiModel { Id = room.Id, Name = room.Name }),
                Addresses = cinema.OrganizationPostalContactItemBridges.Select(bridge => new PostalContactApiModel { Id = bridge.ContactItem.Id, Label = bridge.ContactItem.Label, Address = bridge.ContactItem.Address }),
                Emails = cinema.OrganizationEmailContactItemBridges.Select(bridge => new EmailContactApiModel { Id = bridge.ContactItem.Id, Label = bridge.ContactItem.Label, EmailAddress = bridge.ContactItem.EmailAddress }),
                Phones = cinema.OrganizationPhoneContactItemBridges.Select(bridge => new PhoneContactApiModel { Id = bridge.ContactItem.Id, Label = bridge.ContactItem.Label, PhoneNumber = bridge.ContactItem.PhoneNumber }),
                Image = ImageApiModelFactory.BuildFor(cinema.Image, mediaFileManager, cached)
            };

            return result;
        }
    }
}
