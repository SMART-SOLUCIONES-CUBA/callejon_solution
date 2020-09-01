namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models
{
    public class CinemaBillboardModel
    {
        public CinemaModel Cinema { get; set; }

        public System.Collections.Generic.List<RoomBillboardModel> RoomBillboards { get; set; }

        public CinemaBillboardModel()
        {
            RoomBillboards = new System.Collections.Generic.List<RoomBillboardModel>();
        }
    }
}
