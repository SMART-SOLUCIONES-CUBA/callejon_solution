using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class GenreDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Genre, int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Import(Genre entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
