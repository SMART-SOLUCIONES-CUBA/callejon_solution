namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities
{
    public class Category : Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public Category()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ParentId")]
        public Category Parent { get; set; }

        public System.Guid ParentId { get; set; }

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
