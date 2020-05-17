namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class ProductionViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public System.TimeSpan Duration { get; set; }

        public int Year { get; set; }

        public string About { get; set; }

        public string Argument { get; set; }

        public Xamarin.Forms.ImageSource PosterImageSource { get; set; }

        public GenreViewModel[] Genres { get; set; }

        public ProducerHouseViewModel[] ProducerHouses { get; set; }

        public string Language { get; set; }

        public string MainCountry { get; set; }

        public string CountriesJoin { get; set; }

        public string Category { get; set; }

        public string[] AwardWinnings { get; set; }

        public int Rate { get; set; }

        public PersonalityViewModel[] Productors { get; set; }

        public PersonalityViewModel[] Directors { get; set; }

        public PersonalityViewModel[] Interpreters { get; set; }

        public PersonalityViewModel[] Screenplayers { get; set; }

        public PersonalityViewModel[] Photographers { get; set; }

        public PersonalityViewModel[] Sounders { get; set; }

        public PersonalityViewModel[] Editors { get; set; }

        public PersonalityViewModel[] ArtisticManagers { get; set; }
    }
}
