namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class FixScreenClientConfigurationRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.FixScreenClientConfiguration, System.Guid>
    {
        public FixScreenClientConfigurationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
        public override bool IsPagingOrderDescending()
        {
            return true;
        }
    }
}
