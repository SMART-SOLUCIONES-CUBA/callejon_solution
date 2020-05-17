using System.Linq;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class ProducerHouseRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.ProducerHouse, long>
    {
        public ProducerHouseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<ProducerHouse> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Image);
        }
    }
}