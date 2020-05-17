using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class CinemaBillboardApiModelFactory
    {
        public static CinemaBillboardApiModel BuildFor(Persistence.Entities.Cinema cinema, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, System.DateTime startDate, System.DateTime endDate, bool cached, bool publicsOnly = true)
        {
            var result = new CinemaBillboardApiModel
            {
                Cinema = CinemaApiModelFactory.BuildFor(cinema, mediaFileManager, cached)
            };

            foreach (var room in cinema.Rooms)
            {
                if (room.Presentations.Count <= 0) continue;

                var roomBillboard = RoomBillboardApiModelFactory.BuildFor(room, mediaFileManager, startDate, endDate, cached, publicsOnly);

                if (roomBillboard.RoomDayBillboards.Count <= 0) continue;

                result.RoomBillboards.Add(roomBillboard);
            }

            return result;
        }
    }
}
