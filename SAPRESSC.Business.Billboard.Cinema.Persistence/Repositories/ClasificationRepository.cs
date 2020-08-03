namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class ClasificationRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Clasification, int>
    {
        public ClasificationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
