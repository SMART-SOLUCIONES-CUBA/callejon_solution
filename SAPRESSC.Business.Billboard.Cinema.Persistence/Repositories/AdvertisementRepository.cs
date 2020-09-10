using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class AdvertisementRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Advertisement, System.Guid>
    {
        public AdvertisementRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Advertisement> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.PlacardImage);
        }
    }
}
