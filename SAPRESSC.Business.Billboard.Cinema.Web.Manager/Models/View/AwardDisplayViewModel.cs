using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class AwardDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Award, int>
    {
        public int Id { get; set; }

        public string Name { get; set;}

        public void Import(Award entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
