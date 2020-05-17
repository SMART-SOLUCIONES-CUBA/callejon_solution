namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class AdviseRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Advise, int>
    {
        public AdviseRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
