namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class TagInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Persistence.Entities.Tag, int>
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        public Persistence.Entities.Tag Export()
        {
            var entity = new Persistence.Entities.Tag();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Tag entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public void Mergue(Persistence.Entities.Tag entity)
        {
            entity.Name = Name;
        }
    }
}
