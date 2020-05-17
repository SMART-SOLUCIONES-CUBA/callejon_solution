using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CategoryDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Category, int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Import(Category entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
