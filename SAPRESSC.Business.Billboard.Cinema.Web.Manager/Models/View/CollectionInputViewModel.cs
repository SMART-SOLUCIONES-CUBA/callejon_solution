namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class CollectionInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Persistence.Entities.Collection, System.Guid>
    {
        public CollectionInputViewModel()
        {
            AvailableCollectionSelectListItems = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            AvailableCategorySelectListItems = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Parent")]
        public System.Guid ParentId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableCollectionSelectListItems { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Category")]
        public System.Guid CategoryId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableCategorySelectListItems { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage"), Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage.")]
        [System.ComponentModel.DataAnnotations.Display(Name = "PosterImage")]
        public Microsoft.AspNetCore.Http.IFormFile PosterImageFile { get; set; }

        public System.Guid PosterImageId { get; set; }

        [Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage.")]
        [System.ComponentModel.DataAnnotations.Display(Name = "PlacardImage")]
        public Microsoft.AspNetCore.Http.IFormFile PlacardImageFile { get; set; }

        public System.Guid PlacardImageId { get; set; }

        public bool ClearPlacard { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "About")]
        public string About { get; set; }

        public Persistence.Entities.Collection Export()
        {
            var entity = new Persistence.Entities.Collection();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Collection entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentId = entity.ParentId;
            CategoryId = entity.CategoryId;
            PosterImageId = entity.PosterImage?.Id ?? System.Guid.Empty;
            PlacardImageId = entity.PlacardImage?.Id ?? System.Guid.Empty;
            About = entity.About;
        }

        public void Mergue(Persistence.Entities.Collection entity)
        {
            entity.Name = Name;
            entity.About = About;

            if (entity.ParentId != ParentId)
            {
                entity.ParentId = ParentId;
            }

            if (entity.CategoryId != CategoryId)
            {
                entity.CategoryId = CategoryId;
            }
        }
    }
}
