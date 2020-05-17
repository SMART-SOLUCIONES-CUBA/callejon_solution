using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class RoomBillboardApiModelFactory
    {
        public static RoomBillboardApiModel BuildFor(Persistence.Entities.Room room, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, System.DateTime startDate, System.DateTime endDate, bool cached, bool publicsOnly)
        {
            var result = new RoomBillboardApiModel
            {
                Room = new RoomApiModel { Id = room.Id, Name = room.Name }
            };

            var pivotDate = startDate;

            while (pivotDate <= endDate)
            {
                var planedPresentations =
                    publicsOnly ?
                    room.Presentations.Where(e => e.Date.Date == pivotDate && e.Enabled).OrderBy(e => e.Date).ToList()
                    :
                    room.Presentations.Where(e => e.Date.Date == pivotDate).OrderBy(e => e.Date).ToList();

                if (planedPresentations.Count > 0)
                {
                    var roomBillbard = new RoomDayBillboardApiModel { Date = pivotDate, Presentations = new System.Collections.Generic.List<PresentationApiModel>() };

                    foreach (var item in planedPresentations)
                    {
                        roomBillbard.Presentations.Add(PresentationApiModelFactory.BuildFor(item, mediaFileManager, cached));
                    }

                    result.RoomDayBillboards.Add(roomBillbard);
                }

                pivotDate = pivotDate.AddDays(1);
            }

            return result;
        }
    }
}
