namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class SearchTagBridge
    {
        public long SearchId { get; set; }

        public Search Search { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
