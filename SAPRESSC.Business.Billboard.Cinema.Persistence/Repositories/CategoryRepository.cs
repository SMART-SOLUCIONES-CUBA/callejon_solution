using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class CategoryRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Category, System.Guid>
    {
        public CategoryRepository(DbContext dbContent) : base(dbContent)
        {
        }

        public override System.Func<Entities.Category, object> GetPagingOrderSelector()
        {
            return entity => entity.GetTreeId();
        }

        protected override System.Linq.IQueryable<Entities.Category> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Children);
        }
    }
}
