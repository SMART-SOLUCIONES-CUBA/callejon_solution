namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CollectionDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Persistence.Entities.Collection, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public CollectionDisplayViewModel Parent { get; set; }

        public CategoryDisplayViewModel Category { get; set; }

        public System.Guid PosterImageId { get; set; }

        public System.Guid PlacardImageId { get; set; }

        public string About { get; set; }

        public virtual string TreeName
        {
            get
            {
                return Parent != null ? string.Format("{0} | {1}", Parent.TreeName, Name) : Name;
            }
        }

        public void Import(Persistence.Entities.Collection entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            PosterImageId = entity.PosterImage?.Id ?? System.Guid.Empty;
            PlacardImageId = entity.PlacardImage?.Id ?? System.Guid.Empty;
            About = entity.About;

            if (entity.Parent != null)
            {
                Parent = new CollectionDisplayViewModel();

                Parent.Import(entity.Parent);
            }

            if (entity.Category != null)
            {
                Category = new CategoryDisplayViewModel();

                Category.Import(entity.Category);
            }
        }
    }
}
