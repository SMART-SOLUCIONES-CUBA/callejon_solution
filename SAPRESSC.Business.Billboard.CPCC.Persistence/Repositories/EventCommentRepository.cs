using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class EventCommentRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.EventComment, System.Guid>
    {
        public EventCommentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.EventComment> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Client).ThenInclude(e => e.Image)
                .Include(e => e.Delations).ThenInclude(e => e.Client).ThenInclude(e => e.Image)
                .Include(e => e.Reference)
                .Include(e => e.Event).ThenInclude(entity => entity.PosterImage);
        }

        public override System.Func<Entities.EventComment, object> GetPagingOrderSelector()
        {
            return entity => entity.Created;
        }

        public override bool IsPagingOrderDescending()
        {
            return true;
        }
    }
}
