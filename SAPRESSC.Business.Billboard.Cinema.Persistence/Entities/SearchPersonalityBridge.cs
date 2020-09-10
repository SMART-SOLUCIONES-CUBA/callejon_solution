namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class SearchPersonalityBridge
    {
        public long SearchId { get; set; }

        public Search Search { get; set; }

        public long PersonalityId { get; set; }

        public Personality Personality { get; set; }
    }
}
