using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api
{
    public class RoomBillboardApiModel
    {
        public RoomApiModel Room { get; set; }

        public List<RoomDayBillboardApiModel> RoomDayBillboards { get; set; }

        public RoomBillboardApiModel()
        {
            RoomDayBillboards = new List<RoomDayBillboardApiModel>();
        }
    }
}
