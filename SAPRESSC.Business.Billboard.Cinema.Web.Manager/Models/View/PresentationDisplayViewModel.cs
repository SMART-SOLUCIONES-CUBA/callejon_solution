using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class PresentationDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Presentation, long>
    {
        public long Id { get; set; }

        public System.DateTime PresentationDate { get; set; }

        public ProductionDisplayViewModel Production { get; set; }

        public Room Room { get; set; }

        public bool Enabled { get; set; }

        public void Import(Presentation entity)
        {
            var productionModel = new ProductionDisplayViewModel();

            productionModel.Import(entity.Production);

            Id = entity.Id;
            PresentationDate = entity.Date;
            Production = productionModel;
            Room = entity.Room;
            Enabled = entity.Enabled;
        }
    }
}
