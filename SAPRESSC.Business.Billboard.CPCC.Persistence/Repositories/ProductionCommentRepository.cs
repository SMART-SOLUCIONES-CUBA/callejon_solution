using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class ProductionCommentRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.ProductionComment, System.Guid>
    {
        public ProductionCommentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.ProductionComment> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Client).ThenInclude(e => e.Image)
                .Include(e => e.Delations).ThenInclude(e => e.Client).ThenInclude(e => e.Image)
                .Include(e => e.Reference)
                .Include(e => e.Production).ThenInclude(entity => entity.Language)
                .Include(e => e.Production).ThenInclude(entity => entity.Country)
                .Include(e => e.Production).ThenInclude(entity => entity.PosterImage)
                .Include(e => e.Production).ThenInclude(entity => entity.Clasification);
        }

        public override System.Func<Entities.ProductionComment, object> GetPagingOrderSelector()
        {
            return entity => entity.Created;
        }

        public override bool IsPagingOrderDescending()
        {
            return true;
        }
    }
}
