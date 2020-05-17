using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class EventRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Event, System.Guid>
    {
        public EventRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.Event> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.PosterImage)
                .Include(entity => entity.PromoVideo).ThenInclude(entity => entity.Thumbnail)
                .Include(entity => entity.EventPersonalityBridges).ThenInclude(bridge => bridge.Personality).ThenInclude(entity => entity.Image);
        }
    }
}
