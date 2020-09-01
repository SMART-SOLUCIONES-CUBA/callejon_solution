namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Collection : Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public Collection()
        {
            Id = System.Guid.NewGuid();
            Productions = new System.Collections.Generic.List<Production>();
            GenreBridges = new System.Collections.Generic.List<CollectionGenreBridge>();
            TagBridges = new System.Collections.Generic.List<CollectionTagBridge>();
            ProducerHouseBridges = new System.Collections.Generic.List<CollectionProducerHouseBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public Clasification Clasification { get; set; }

        public Middleware.Media.Persistence.Entities.Image PosterImage { get; set; }

        public Middleware.Media.Persistence.Entities.Image PlacardImage { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ParentId")]
        public Collection Parent { get; set; }

        public System.Guid ParentId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public System.Guid CategoryId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string About { get; set; }

        public System.Collections.Generic.List<Production> Productions { get; set; }

        public System.Collections.Generic.List<CollectionGenreBridge> GenreBridges { get; set; }

        public System.Collections.Generic.List<CollectionTagBridge> TagBridges { get; set; }

        public System.Collections.Generic.List<CollectionProducerHouseBridge> ProducerHouseBridges { get; set; }

        public virtual string GetTreeName()
        {
            return Parent != null ? string.Format("{0} | {1}", Parent.GetTreeName(), Name) : Name;
        }

        public virtual string GetTreeId()
        {
            return Parent != null ? string.Format("{0}_{1}", Parent.GetTreeId(), Id) : Id.ToString();
        }
    }
}
