namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class SearchGenreBridge
    {
        public long SearchId { get; set; }

        public Search Search { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
