using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class ParticipationDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Participation, long>
    {
        public long Id { get; set; }

        public Persistence.Enumerations.ProductionRole ProductionRole { get; set; }

        public Personality Personality { get; set; }

        public long ProductionId { get; set; }

        public string Specification { get; set; }

        public void Import(Participation entity)
        {
            Id = entity.Id;
            ProductionRole = entity.Role;
            Personality = entity.Personality;
            ProductionId = entity.Production.Id;
            Specification = entity.Specification;
        }
    }
}
