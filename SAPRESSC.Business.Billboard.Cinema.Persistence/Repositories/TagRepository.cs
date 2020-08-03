namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class TagRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Tag, int>
    {
        public TagRepository(Microsoft.EntityFrameworkCore.DbContext dbContent) : base(dbContent)
        {
        }
    }
}
