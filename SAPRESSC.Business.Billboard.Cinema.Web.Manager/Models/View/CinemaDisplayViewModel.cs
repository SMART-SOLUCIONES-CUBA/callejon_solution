using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CinemaDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Persistence.Entities.Cinema, long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public PostalContactItem PostalContact { get; set; }

        public double LocalizationLongitude { get; set; }

        public double LocalizationLatitude { get; set; }

        public string About { get; set; }

        public System.Guid ImageId { get; set; }

        public ICollection<EmailContactItem> EmailContacts { get; set; }

        public ICollection<PhoneContactItem> PhoneContacts { get; set; }

        public ICollection<Persistence.Entities.Room> Rooms { get; set; }

        public void Import(Persistence.Entities.Cinema entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            PostalContact = entity.GetPostalContactItems().FirstOrDefault();
            LocalizationLongitude = entity.LocalizationLongitude;
            LocalizationLatitude = entity.LocalizationLatitude;
            About = entity.About;
            ImageId = entity.Image != null ? entity.Image.Id : System.Guid.Empty;
            EmailContacts = entity.GetEmailContactItems();
            PhoneContacts = entity.GetPhoneContactItems();
            Rooms = entity.Rooms ?? (new Persistence.Entities.Room[] { });
        }
    }
}
