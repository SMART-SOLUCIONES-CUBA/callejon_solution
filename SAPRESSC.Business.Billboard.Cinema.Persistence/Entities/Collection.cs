namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Collection : Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public Collection()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

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
