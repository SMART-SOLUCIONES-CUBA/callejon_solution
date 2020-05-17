namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class ClientRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Client, System.Guid>
    {
        public ClientRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
