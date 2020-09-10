using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class SearchRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Search, long>
    {
        public SearchRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Search> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Category)
                .Include(entity => entity.Country)
                .Include(entity => entity.ProducerHouse)
                .Include(entity => entity.GenreBridges).ThenInclude(bridge => bridge.Genre)
                .Include(entity => entity.PersonalityBridges).ThenInclude(bridge => bridge.Personality)
                .Include(entity => entity.TagBridges).ThenInclude(bridge => bridge.Tag);
        }

        public override System.Func<Entities.Search, object> GetPagingOrderSelector()
        {
            return entity => entity.At;
        }

        public override bool IsPagingOrderDescending()
        {
            return true;
        }
    }
}
