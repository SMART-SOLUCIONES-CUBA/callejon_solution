namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class ProductionCheckout : Core.Persistence.Entities.IEntity<long>
    {
        public ProductionCheckout()
        {
            At = System.DateTime.Now;
        }

        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string SubscriptorId { get; set; }

        public System.DateTime At { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ProductionId")]
        public Production Production { get; set; }

        public long ProductionId { get; set; }
    }
}
