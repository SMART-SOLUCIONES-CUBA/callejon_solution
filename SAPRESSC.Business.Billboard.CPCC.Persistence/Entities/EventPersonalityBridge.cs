namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    public class EventPersonalityBridge
    {
        public System.Guid EventId { get; set; }

        public Event Event { get; set; }

        public long PersonalityId { get; set; }

        public Cinema.Persistence.Entities.Personality Personality { get; set; }

        public bool IsPresenter { get; set; }
    }
}
