using System.Linq;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class RoomRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Room, System.Guid>
    {
        public RoomRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Room> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(item => item.Cinema)
                .Include(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(pro => pro.Participations).ThenInclude(bridge => bridge.Personality)
                .Include(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(pro => pro.Language)
                .Include(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(pro => pro.ProductionGenreBridges).ThenInclude(bridge => bridge.Genre)
                .Include(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(pro => pro.ProducerHouseProductionBridges).ThenInclude(bridge => bridge.ProducerHouse)
                .Include(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(pro => pro.Country)
                .Include(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(pro => pro.AwardWinnings).ThenInclude(aw => aw.Award)
                .Include(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(pro => pro.Category)
                .Include(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(pro => pro.PosterImage);
        }
    }
}
