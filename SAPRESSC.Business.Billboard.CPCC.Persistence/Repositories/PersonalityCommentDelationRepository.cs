using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class PersonalityCommentDelationRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.PersonalityCommentDelation, System.Guid>
    {
        public PersonalityCommentDelationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.PersonalityCommentDelation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Comment)
                .Include(entity => entity.Client);
        }
    }
}
