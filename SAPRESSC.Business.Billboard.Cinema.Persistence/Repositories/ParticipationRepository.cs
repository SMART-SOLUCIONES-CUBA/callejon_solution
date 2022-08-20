using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class ParticipationRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Participation, long>
    {
        public ParticipationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Participation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Personality).ThenInclude(e => e.Image);
        }
    }
}
