namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class WithInDisplayViewModel
    {
        public System.Guid EventId { get; set; }

        public Persistence.Entities.Event Event { get; set; }

        public long PersonalityId { get; set; }

        public Cinema.Persistence.Entities.Personality Personality { get; set; }

        public bool IsPresenter { get; set; }
    }
}
