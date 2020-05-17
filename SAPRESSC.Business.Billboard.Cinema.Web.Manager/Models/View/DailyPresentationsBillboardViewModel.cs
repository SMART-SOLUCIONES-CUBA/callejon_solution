using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class DailyPresentationsBillboardViewModel
    {
        public Room Room { get; set; }

        public System.DateTime Date { get; set; }

        public ICollection<Presentation> Presentations { get; set; }

        public DailyPresentationsBillboardViewModel()
        {
            Presentations = new List<Presentation>();
        }
    }
}
