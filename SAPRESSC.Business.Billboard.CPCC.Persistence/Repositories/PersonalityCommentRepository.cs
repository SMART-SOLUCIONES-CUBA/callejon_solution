using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class PersonalityCommentRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.PersonalityComment, System.Guid>
    {
        public PersonalityCommentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.PersonalityComment> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Client).ThenInclude(e => e.Image)
                .Include(e => e.Delations).ThenInclude(e => e.Client).ThenInclude(e => e.Image)
                .Include(e => e.Reference)
                .Include(e => e.Personality).ThenInclude(e => e.Image);
        }

        public override System.Func<Entities.PersonalityComment, object> GetPagingOrderSelector()
        {
            return entity => entity.Created;
        }

        public override bool IsPagingOrderDescending()
        {
            return true;
        }
    }
}
