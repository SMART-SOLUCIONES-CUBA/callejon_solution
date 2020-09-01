namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class CollectionCheckout : Core.Persistence.Entities.IEntity<long>
    {
        public CollectionCheckout()
        {
            At = System.DateTime.Now;
        }

        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string SubscriptorId { get; set; }

        public System.DateTime At { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CollectionId")]
        public Collection Collection { get; set; }

        public System.Guid CollectionId { get; set; }
    }
}
