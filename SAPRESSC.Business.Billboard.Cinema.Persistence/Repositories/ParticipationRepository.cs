namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class ParticipationRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Participation, long>
    {
        public ParticipationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
