using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class AwardWinningDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<AwardWinning, int>
    {
        public int Id { get; set; }

        public string Edition { get; set; }

        public AwardWinType WinType { get; set; }

        public Award Award { get; set; }

        public string Category { get; set; }

        public long ProductionId { get; set; }

        public void Import(AwardWinning entity)
        {
            Id = entity.Id;
            Edition = entity.Edition;
            WinType = entity.Type;
            Award = entity.Award;
            Category = entity.Category;
            ProductionId = entity.Production.Id;
        }
    }
}
