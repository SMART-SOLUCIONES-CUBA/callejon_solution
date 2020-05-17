using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Cinema : Middleware.Identity.Persistence.Entities.Organization
    {
        public Cinema()
        {
            Rooms = new List<Room>();
        }

        public double LocalizationLongitude { get; set; }

        public double LocalizationLatitude { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
