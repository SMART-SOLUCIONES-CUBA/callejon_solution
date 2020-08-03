namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CategoryInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Persistence.Entities.Category, System.Guid>
    {
        public CategoryInputViewModel()
        {
            AvailableCategorySelectListItems = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Parent")]
        public System.Guid ParentId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableCategorySelectListItems { get; set; }

        public Persistence.Entities.Category Export()
        {
            var entity = new Persistence.Entities.Category();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Category entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentId = entity.ParentId;
        }

        public void Mergue(Persistence.Entities.Category entity)
        {
            entity.Name = Name;

            if (entity.ParentId != ParentId)
            {
                entity.ParentId = ParentId;
            }
        }
    }
}
