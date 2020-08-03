using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class ClasificationDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Clasification, int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Import(Clasification entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
