using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class ProductionCommentDelationRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.ProductionCommentDelation, System.Guid>
    {
        public ProductionCommentDelationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.ProductionCommentDelation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Comment)
                .Include(entity => entity.Client);
        }
    }
}
