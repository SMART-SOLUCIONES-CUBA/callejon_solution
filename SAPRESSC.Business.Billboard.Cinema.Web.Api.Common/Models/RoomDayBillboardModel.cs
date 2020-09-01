using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class RoomDayBillboardModel
    {
        public System.DateTime Date { get; set; }

        public List<PresentationModel> Presentations { get; set; }

        public RoomDayBillboardModel()
        {
            Presentations = new List<PresentationModel>();
        }
    }
}
