namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class FetchOrder : Core.Persistence.Entities.IEntity<long>
    {
        public FetchOrder()
        {
            At = System.DateTime.Now;

            ProductionBridges = new System.Collections.Generic.List<FetchOrderProductionBridge>();
            CollectionBridges = new System.Collections.Generic.List<FetchOrderCollectionBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        public System.DateTime At { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string SubscriptorId { get; set; }

        public System.Collections.Generic.List<FetchOrderProductionBridge> ProductionBridges { get; set; }

        public System.Collections.Generic.List<FetchOrderCollectionBridge> CollectionBridges { get; set; }
    }
}
