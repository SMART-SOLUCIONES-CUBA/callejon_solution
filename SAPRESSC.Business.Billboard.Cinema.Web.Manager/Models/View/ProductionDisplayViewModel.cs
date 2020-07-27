using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class ProductionDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Production, long>
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Slug { get => Providers.SlugHelperProvider.Instance.Get().GenerateSlug(Title); }

        public string LocalizedTitle { get; set; }

        public System.TimeSpan Duration { get; set; }

        public int Year { get; set; }

        public string About { get; set; }

        public string Argument { get; set; }

        public Middleware.Region.Persistence.Entities.Country Country { get; set; }

        public Language Language { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<ProducerHouse> ProducerHouses { get; set; }

        public ICollection<AwardWinning> AwardWinnings { get; set; }

        public ICollection<Personality> Productors { get; set; }

        public ICollection<Personality> Directors { get; set; }

        public ICollection<Personality> Interpreters { get; set; }

        public ICollection<Personality> Screenplayers { get; set; }

        public ICollection<Personality> Photographers { get; set; }

        public ICollection<Personality> Sounders { get; set; }

        public ICollection<Personality> Editors { get; set; }

        public ICollection<Personality> ArtisticManagers { get; set; }

        public Category Category { get; set; }

        public int Rate { get; set; }

        public System.Guid PosterImageId { get; set; }

        public System.Guid TrailerVideoId { get; set; }

        public string TrailerVideoExtension { get; set; }

        public void Import(Production entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            LocalizedTitle = entity.LocalizedTitle;
            Duration = System.TimeSpan.FromMinutes(entity.Duration);
            Year = entity.Year;
            About = entity.About;
            Argument = entity.Argument;
            Country = entity.Country;
            Language = entity.Language;
            Genres = entity.GetGenres();
            ProducerHouses = entity.GetProducerHouses();
            AwardWinnings = entity.AwardWinnings;
            Productors = entity.Participations.Where(p => p.Role == ProductionRole.Production).Select(p => p.Personality).ToArray();
            Directors = entity.Participations.Where(p => p.Role == ProductionRole.Direction).Select(p => p.Personality).ToArray();
            Interpreters = entity.Participations.Where(p => p.Role == ProductionRole.Interpretation).Select(p => p.Personality).ToArray();
            Screenplayers = entity.Participations.Where(p => p.Role == ProductionRole.Screenplay).Select(p => p.Personality).ToArray();
            Photographers = entity.Participations.Where(p => p.Role == ProductionRole.Photography).Select(p => p.Personality).ToArray();
            Sounders = entity.Participations.Where(p => p.Role == ProductionRole.Sound).Select(p => p.Personality).ToArray();
            Editors = entity.Participations.Where(p => p.Role == ProductionRole.Edition).Select(p => p.Personality).ToArray();
            ArtisticManagers = entity.Participations.Where(p => p.Role == ProductionRole.ArtisticManagement).Select(p => p.Personality).ToArray();
            Category = entity.Category;
            Rate = entity.Rate;
            PosterImageId = entity.PosterImage != null ? entity.PosterImage.Id : System.Guid.Empty;
            TrailerVideoId = entity.TrailerVideo != null ? entity.TrailerVideo.Id : System.Guid.Empty;
            TrailerVideoExtension = entity.TrailerVideo != null ? entity.TrailerVideo.Extension : string.Empty;
        }
    }
}
