namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CategoryDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Persistence.Entities.Category, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Slug { get => Common.Providers.SlugHelperProvider.Instance.Get().GenerateSlug(Name); }

        public CategoryDisplayViewModel Parent { get; set; }

        public virtual string TreeName
        {
            get
            {
                return Parent != null ? string.Format("{0} | {1}", Parent.TreeName, Name) : Name;
            }
        }

        public void Import(Persistence.Entities.Category entity)
        {
            Id = entity.Id;
            Name = entity.Name;

            if (entity.Parent != null)
            {
                Parent = new CategoryDisplayViewModel();

                Parent.Import(entity.Parent);
            }
        }
    }
}
