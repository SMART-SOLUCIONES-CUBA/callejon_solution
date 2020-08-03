using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class CollectionRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Collection, System.Guid>
    {
        public CollectionRepository(DbContext dbContent) : base(dbContent)
        {
        }

        protected override System.Linq.IQueryable<Entities.Collection> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.PosterImage)
                .Include(entity => entity.PlacardImage);
        }

        public override System.Func<Entities.Collection, object> GetPagingOrderSelector()
        {
            return entity => entity.GetTreeId();
        }
    }
}
