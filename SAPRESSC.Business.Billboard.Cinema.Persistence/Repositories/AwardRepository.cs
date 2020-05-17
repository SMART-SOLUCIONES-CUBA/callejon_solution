namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class AwardRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Award, int>
    {
        public AwardRepository(Microsoft.EntityFrameworkCore.DbContext dbContent) : base(dbContent)
        {
        }
    }
}