using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class SubscriptionRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Subscription, string>
    {
        public SubscriptionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Subscription> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.PreferedTagBridges).ThenInclude(bridge => bridge.Tag)
                .Include(entity => entity.PreferedGenreBridges).ThenInclude(bridge => bridge.Genre)
                .Include(entity => entity.PreferedProducerHouseBridges).ThenInclude(bridge => bridge.ProducerHouse).ThenInclude(entity => entity.Image);
        }
    }
}