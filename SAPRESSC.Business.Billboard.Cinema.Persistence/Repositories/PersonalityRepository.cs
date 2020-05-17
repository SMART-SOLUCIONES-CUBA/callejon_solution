using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class PersonalityRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Personality, long>
    {
        public PersonalityRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Personality> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Image);
        }
    }
}
