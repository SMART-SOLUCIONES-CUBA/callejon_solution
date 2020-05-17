namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class EventDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Persistence.Entities.Event, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Header { get; set; }

        public string Title { get; set; }

        public bool Enabled { get; set; }

        public System.DateTime Start { get; set; }

        public System.DateTime End { get; set; }

        public string Place { get; set; }

        public string Thematic { get; set; }

        public string Organizer { get; set; }

        public System.Collections.Generic.ICollection<Cinema.Persistence.Entities.Personality> Invitations { get; set; }

        public string About { get; set; }

        public string Description { get; set; }

        public System.Guid PosterImageId { get; set; }

        public System.Guid PromoVideoId { get; set; }

        public string PromoVideoExtension { get; set; }

        public void Import(Persistence.Entities.Event entity)
        {
            Id = entity.Id;
            Header = entity.Header;
            Title = entity.Title;
            Enabled = entity.Enabled;
            Start = entity.Start;
            End = entity.End;
            Place = entity.Place;
            Thematic = entity.Thematic;
            Organizer = entity.Organizer;
            Invitations = entity.GetPersonalities();
            About = entity.About;
            Description = entity.Description;
            PosterImageId = entity.PosterImage.Id;
            PromoVideoId = entity.PromoVideo != null ? entity.PromoVideo.Id : System.Guid.Empty;
            PromoVideoExtension = entity.PromoVideo != null ? entity.PromoVideo.Extension : string.Empty;
        }
    }
}
