using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class FetchOrderRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.FetchOrder, long>
    {
        public FetchOrderRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.FetchOrder> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.ProductionBridges).ThenInclude(bridge => bridge.Production).ThenInclude(entity => entity.PosterImage)
                .Include(entity => entity.ProductionBridges).ThenInclude(bridge => bridge.Production).ThenInclude(entity => entity.PlacardImage);
        }
    }
}
