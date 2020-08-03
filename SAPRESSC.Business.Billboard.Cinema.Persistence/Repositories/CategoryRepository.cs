namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class CategoryRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Category, System.Guid>
    {
        public CategoryRepository(Microsoft.EntityFrameworkCore.DbContext dbContent) : base(dbContent)
        {
        }

        public override System.Func<Entities.Category, object> GetPagingOrderSelector()
        {
            return entity => entity.GetTreeId();
        }
    }
}
