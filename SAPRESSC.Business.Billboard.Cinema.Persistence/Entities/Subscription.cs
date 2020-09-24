using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Subscription : Core.Persistence.Entities.IEntity<string>
    {
        public Subscription()
        {
            PreferedProducerHouseBridges = new List<SubscriptionProducerHouseBridge>();
            PreferedGenreBridges = new List<SubscriptionGenreBridge>();
            PreferedTagBridges = new List<SubscriptionTagBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }

        public List<SubscriptionProducerHouseBridge> PreferedProducerHouseBridges { get; set; }

        public List<SubscriptionGenreBridge> PreferedGenreBridges { get; set; }

        public List<SubscriptionTagBridge> PreferedTagBridges { get; set; }
    }
}
