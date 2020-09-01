using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models
{
    public class EventModel
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

        public System.Collections.Generic.ICollection<PersonalityModel> Presenters { get; set; }

        public System.Collections.Generic.ICollection<PersonalityModel> WithIns { get; set; }

        public string About { get; set; }

        public string Description { get; set; }

        public ImageModel PosterImage { get; set; }

        public VideoModel PromoVideo { get; set; }

        public EventModel()
        {
            Presenters = new System.Collections.Generic.List<PersonalityModel>();
            WithIns = new System.Collections.Generic.List<PersonalityModel>();
        }
    }
}
