namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class CategoryRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Category, int>
    {
        public CategoryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
