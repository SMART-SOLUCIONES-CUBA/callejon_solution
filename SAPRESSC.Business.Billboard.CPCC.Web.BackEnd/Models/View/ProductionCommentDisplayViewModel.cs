namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class ProductionCommentDisplayViewModel : AbstractCommentDisplayViewModel<Persistence.Entities.ProductionComment>
    {
        public ProductionCommentDisplayViewModel()
        {
            Production = new Cinema.Web.Manager.Models.View.ProductionDisplayViewModel();
        }

        public Cinema.Web.Manager.Models.View.ProductionDisplayViewModel Production { get; set; }

        public ProductionCommentDisplayViewModel Reference { get; set; }

        public override void Import(Persistence.Entities.ProductionComment entity)
        {
            base.Import(entity);

            DelationCount = entity.Delations.Count;

            Production.Import(entity.Production);

            if (entity.Reference != null)
            {
                Reference = new ProductionCommentDisplayViewModel();

                Reference.Import(entity.Reference);
            }
        }
    }
}
