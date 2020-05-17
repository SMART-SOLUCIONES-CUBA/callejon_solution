namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class LanguageRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Language, int>
    {
        public LanguageRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) :base(dbContext)
        {
        }
    }
}
