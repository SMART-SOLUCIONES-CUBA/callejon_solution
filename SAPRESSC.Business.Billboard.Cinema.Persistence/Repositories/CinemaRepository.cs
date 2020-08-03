using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class CinemaRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Cinema, long>
    {
        public CinemaRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.Cinema> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Image)
                .Include(entity => entity.OrganizationPhoneContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.OrganizationEmailContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.OrganizationPostalContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.Rooms).ThenInclude(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(entity => entity.ProductionGenreBridges).ThenInclude(bridge => bridge.Genre)
                .Include(entity => entity.Rooms).ThenInclude(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(entity => entity.ProducerHouseProductionBridges).ThenInclude(bridge => bridge.ProducerHouse)
                .Include(entity => entity.Rooms).ThenInclude(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(entity => entity.Language)
                .Include(entity => entity.Rooms).ThenInclude(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(entity => entity.Country)
                .Include(entity => entity.Rooms).ThenInclude(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(entity => entity.AwardWinnings).ThenInclude(relation => relation.Award)
                .Include(entity => entity.Rooms).ThenInclude(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(entity => entity.Participations).ThenInclude(relation => relation.Personality).ThenInclude(entity => entity.Image)
                .Include(entity => entity.Rooms).ThenInclude(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(entity => entity.PosterImage)
                .Include(entity => entity.Rooms).ThenInclude(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(entity => entity.TrailerVideo)
                .Include(entity => entity.Rooms).ThenInclude(r => r.Presentations).ThenInclude(pre => pre.Production).ThenInclude(entity => entity.Clasification);
        }
    }
}
