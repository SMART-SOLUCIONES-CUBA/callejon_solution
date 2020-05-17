using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class PersonalityDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Personality, long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        public System.DateTime BornDate { get; set; }

        public string About { get; set; }

        public System.Guid ImageId { get; set; }

        public void Import(Personality entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            LastName = entity.LastName;
            Sex = entity.Sex;
            BornDate = entity.BornDate;
            About = entity.About;
            ImageId = entity.Image != null ? entity.Image.Id : System.Guid.Empty;
        }
    }
}

