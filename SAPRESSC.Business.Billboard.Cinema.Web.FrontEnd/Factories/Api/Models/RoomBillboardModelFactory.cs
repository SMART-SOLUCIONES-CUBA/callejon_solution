using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.FrontEnd.Factories.Api.Models
{
    public static class RoomBillboardModelFactory
    {
        public static RoomBillboardModel BuildFor(Persistence.Entities.Room room, Middleware.Media.Web.Common.Facades.Manager.IMediaFileManagerFacade mediaFileManager, System.DateTime startDate, System.DateTime endDate, bool cached, bool publicsOnly)
        {
            var result = new RoomBillboardModel
            {
                Room = new RoomModel { Id = room.Id, Name = room.Name }
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
                    var roomBillbard = new RoomDayBillboardModel { Date = pivotDate, Presentations = new System.Collections.Generic.List<PresentationModel>() };

                    foreach (var item in planedPresentations)
                    {
                        roomBillbard.Presentations.Add(PresentationModelFactory.BuildFor(item, mediaFileManager, cached));
                    }

                    result.RoomDayBillboards.Add(roomBillbard);
                }

                pivotDate = pivotDate.AddDays(1);
            }

            return result;
        }
    }
}
