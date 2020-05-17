using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api
{
    public class RoomDayBillboardApiModel
    {
        public System.DateTime Date { get; set; }

        public List<PresentationApiModel> Presentations { get; set; }

        public RoomDayBillboardApiModel()
        {
            Presentations = new List<PresentationApiModel>();
        }
    }
}
