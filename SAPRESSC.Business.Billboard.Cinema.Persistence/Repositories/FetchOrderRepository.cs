namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class FetchOrderRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.FetchOrder, long>
    {
        public FetchOrderRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
