namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Repositories
{
    public class CommentClassifierRepository : Core.Persistence.Repositories.AbstractEntityRepository<Entities.CommentClassifier, int>
    {
        public CommentClassifierRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
