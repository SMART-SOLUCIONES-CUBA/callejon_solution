using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Search : Core.Persistence.Entities.IEntity<long>
    {
        public Search()
        {
            At = System.DateTime.Now;

            GenreBridges = new List<SearchGenreBridge>();
            PersonalityBridges = new List<SearchPersonalityBridge>();
            TagBridges = new List<SearchTagBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        public System.DateTime At { get; set; }

        public string SubscriptionId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("SubscriptionId")]
        public Subscription Subscription { get; set; }

        public string TitleTemathic { get; set; }

        public Category Category { get; set; }

        public int Year { get; set; }

        public Middleware.Region.Persistence.Entities.Country Country { get; set; }

        public ProducerHouse ProducerHouse { get; set; }

        public List<SearchGenreBridge> GenreBridges { get; set; }

        public List<SearchPersonalityBridge> PersonalityBridges { get; set; }

        public List<SearchTagBridge> TagBridges { get; set; }
    }
}
