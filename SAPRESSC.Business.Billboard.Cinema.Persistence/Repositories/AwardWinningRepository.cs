namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class AwardWinningRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.AwardWinning, int>
    {
        public AwardWinningRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
