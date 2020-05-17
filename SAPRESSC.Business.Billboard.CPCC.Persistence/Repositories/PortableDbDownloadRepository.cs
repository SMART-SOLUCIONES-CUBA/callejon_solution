namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class PortableDbDownloadRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.PortableDbDownload, long>
    {
        public PortableDbDownloadRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
