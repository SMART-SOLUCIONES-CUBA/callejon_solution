using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Production : Core.Persistence.Entities.IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string LocalizedTitle { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string About { get; set; }

        public string Argument { get; set; }

        [Required]
        public Middleware.Media.Persistence.Entities.Image PosterImage { get; set; }

        public Middleware.Media.Persistence.Entities.Image PlacardImage { get; set; }

        public Middleware.Media.Persistence.Entities.Video TrailerVideo { get; set; }

        public ICollection<ProductionGenreBridge> ProductionGenreBridges { get; set; }

        public ICollection<ProducerHouseProductionBridge> ProducerHouseProductionBridges { get; set; }

        public ICollection<CountryCoProductionBridge> CountryCoProductionBridges { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public Middleware.Region.Persistence.Entities.Country Country { get; set; }

        [Required]
        public Clasification Clasification { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public System.Guid CategoryId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CollectionId")]
        public Collection Collection { get; set; }

        public System.Guid CollectionId { get; set; }

        public ICollection<ProductionTagBridge> TagBridges { get; set; }

        public ICollection<AwardWinning> AwardWinnings { get; set; }

        public ICollection<Participation> Participations { get; set; }

        [Required, Range(1, 5)]
        public int Rate { get; set; }

        public Production()
        {
            ProductionGenreBridges = new List<ProductionGenreBridge>();
            ProducerHouseProductionBridges = new List<ProducerHouseProductionBridge>();
            AwardWinnings = new List<AwardWinning>();
            Participations = new List<Participation>();
            CountryCoProductionBridges = new List<CountryCoProductionBridge>();
        }

        public ICollection<Genre> GetGenres()
        {
            var result = new List<Genre>();

            foreach (var bridge in ProductionGenreBridges)
            {
                result.Add(bridge.Genre);
            }

            return result;
        }

        public ICollection<ProducerHouse> GetProducerHouses()
        {
            var result = new List<ProducerHouse>();

            foreach (var bridge in ProducerHouseProductionBridges)
            {
                result.Add(bridge.ProducerHouse);
            }

            return result;
        }

        public void SetGenres(ICollection<Genre> genres)
        {
            ProductionGenreBridges.Clear();

            if (genres == null) return;

            foreach (var item in genres)
            {
                ProductionGenreBridges.Add(new ProductionGenreBridge
                {
                    ProductionId = Id,
                    Production = this,
                    GenreId = item.Id,
                    Genre = item
                });
            }
        }

        public void SetProducerHouses(ICollection<ProducerHouse> producerHouses)
        {
            ProducerHouseProductionBridges.Clear();

            if (producerHouses == null) return;

            foreach (var item in producerHouses)
            {
                ProducerHouseProductionBridges.Add(new ProducerHouseProductionBridge
                {
                    ProductionId = Id,
                    Production = this,
                    ProducerHouseId = item.Id,
                    ProducerHouse = item
                });
            }
        }
    }
}
