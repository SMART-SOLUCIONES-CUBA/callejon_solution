using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class RoomBillboardModel
    {
        public RoomModel Room { get; set; }

        public List<RoomDayBillboardModel> RoomDayBillboards { get; set; }

        public RoomBillboardModel()
        {
            RoomDayBillboards = new List<RoomDayBillboardModel>();
        }
    }
}
