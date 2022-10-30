using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Subscription : Core.Persistence.Entities.IHabilitableEntity<string>
    {
        public Subscription()
        {
            Identity = new Middleware.Identity.Persistence.Entities.Person();
            ServiceAvailabilityFrom = System.DateTime.Now;
            PreferedProducerHouseBridges = new List<SubscriptionProducerHouseBridge>();
            PreferedGenreBridges = new List<SubscriptionGenreBridge>();
            PreferedTagBridges = new List<SubscriptionTagBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }

        public bool Enabled { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }

        public bool PasswordChangeRequired { get; set; }

        public System.DateTime? LastSignInAt { get; set; }

        public int FailAuthenticationCount { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdentityId")]
        public Middleware.Identity.Persistence.Entities.Person Identity { get; set; }

        public long IdentityId { get; set; }

        public System.DateTime ServiceAvailabilityFrom { get; set; }

        public System.DateTime ServiceAvailabilityTo { get; set; }

        public List<SubscriptionProducerHouseBridge> PreferedProducerHouseBridges { get; set; }

        public List<SubscriptionGenreBridge> PreferedGenreBridges { get; set; }

        public List<SubscriptionTagBridge> PreferedTagBridges { get; set; }
    }
}
