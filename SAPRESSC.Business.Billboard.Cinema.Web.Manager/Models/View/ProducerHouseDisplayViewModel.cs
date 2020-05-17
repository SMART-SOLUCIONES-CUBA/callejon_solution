using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class ProducerHouseDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<ProducerHouse, long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string About { get; set; }

        public System.Guid ImageId { get; set; }

        public void Import(ProducerHouse entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            About = entity.About;
            ImageId = entity.Image != null ? entity.Image.Id : System.Guid.Empty;
        }
    }
}
