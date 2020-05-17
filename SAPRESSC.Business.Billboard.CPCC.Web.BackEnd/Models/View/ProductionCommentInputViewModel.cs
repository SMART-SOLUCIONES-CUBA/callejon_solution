namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class ProductionCommentInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Persistence.Entities.ProductionComment, System.Guid>
    {
        public System.Guid Id { get; set; }

        public Persistence.Entities.ProductionComment Export()
        {
            throw new System.NotImplementedException();
        }

        public void Import(Persistence.Entities.ProductionComment entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(Persistence.Entities.ProductionComment entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
