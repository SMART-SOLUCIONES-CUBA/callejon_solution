using System.Linq;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class PresentationRepository : Core.Persistence.Repositories.AbstractEntityRepository<Presentation, long>
    {
        public PresentationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Presentation, object> GetPagingOrderSelector()
        {
            return p => p.Date;
        }

        protected override IQueryable<Presentation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Production).ThenInclude(prod => prod.Country)
                .Include(entity => entity.Room).ThenInclude(item => item.Cinema);
        }
    }
}
