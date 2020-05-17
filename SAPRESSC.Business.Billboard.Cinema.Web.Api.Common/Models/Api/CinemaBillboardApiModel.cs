using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api
{
    public class CinemaBillboardApiModel
    {
        public CinemaApiModel Cinema { get; set; }

        public List<RoomBillboardApiModel> RoomBillboards { get; set; }

        public CinemaBillboardApiModel()
        {
            RoomBillboards = new List<RoomBillboardApiModel>();
        }
    }
}
