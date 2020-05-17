using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class ProductionRepository : Core.Persistence.Repositories.AbstractEntityRepository<Production, long>
    {
        public ProductionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Production> GetQueryableDbSet()
        {
            return DbContext.Set<Production>()
                .Include(entity => entity.ProductionGenreBridges).ThenInclude(bridge => bridge.Genre)
                .Include(entity => entity.ProducerHouseProductionBridges).ThenInclude(bridge => bridge.ProducerHouse)
                .Include(entity => entity.Language)
                .Include(entity => entity.Country)
                .Include(entity => entity.CountryCoProductionBridges).ThenInclude(bridge => bridge.Country)
                .Include(entity => entity.AwardWinnings).ThenInclude(relation => relation.Award)
                .Include(entity => entity.Participations).ThenInclude(relation => relation.Personality)
                .Include(entity => entity.PosterImage)
                .Include(entity => entity.TrailerVideo).ThenInclude(entity => entity.Thumbnail)
                .Include(entity => entity.Category);
        }
    }
}
