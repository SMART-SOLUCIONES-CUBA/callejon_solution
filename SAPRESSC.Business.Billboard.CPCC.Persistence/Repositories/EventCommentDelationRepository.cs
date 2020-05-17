using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class EventCommentDelationRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.EventCommentDelation, System.Guid>
    {
        public EventCommentDelationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.EventCommentDelation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Comment)
                .Include(entity => entity.Client);
        }
    }
}
