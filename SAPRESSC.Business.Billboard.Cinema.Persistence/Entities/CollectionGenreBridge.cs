namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class CollectionGenreBridge
    {
        public System.Guid CollectionId { get; set; }

        public Collection Collection { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
