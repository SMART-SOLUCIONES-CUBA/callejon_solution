namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Repositories
{
    public class GenreRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.Genre, int>
    {
        public GenreRepository(Microsoft.EntityFrameworkCore.DbContext dbContent) : base(dbContent)
        {
        }
    }
}
