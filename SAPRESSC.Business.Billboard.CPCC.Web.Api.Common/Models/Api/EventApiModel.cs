using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.Api;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.Api.Common.Models.Api
{
    public class EventApiModel
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

        public System.Collections.Generic.ICollection<PersonalityApiModel> Presenters { get; set; }

        public System.Collections.Generic.ICollection<PersonalityApiModel> WithIns { get; set; }

        public string About { get; set; }

        public string Description { get; set; }

        public ImageApiModel PosterImage { get; set; }

        public VideoApiModel PromoVideo { get; set; }

        public EventApiModel()
        {
            Presenters = new System.Collections.Generic.List<PersonalityApiModel>();
            WithIns = new System.Collections.Generic.List<PersonalityApiModel>();
        }
    }
}
