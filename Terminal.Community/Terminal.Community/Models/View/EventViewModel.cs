namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Models.View
{
    public class EventViewModel
    {
        public System.Guid Id { get; set; }

        public string Header { get; set; }
        
        public string Title { get; set; }

        public System.DateTime Start { get; set; }

        public System.DateTime End { get; set; }
        
        public string Place { get; set; }

        public string Thematic { get; set; }

        public string Organizer { get; set; }

        public PersonalityViewModel[] Presenters { get; set; }

        public PersonalityViewModel[] Guests { get; set; }
        
        public string About { get; set; }

        public string Description { get; set; }

        public System.Guid PosterImageId { get; set; }
    }
}
