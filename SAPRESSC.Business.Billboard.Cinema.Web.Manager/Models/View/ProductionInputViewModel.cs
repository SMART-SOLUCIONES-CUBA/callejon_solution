using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class ProductionInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Production, long>
    {
        public ProductionInputViewModel()
        {
            Year = System.DateTime.Now.Year;
            AvailableCategorySelectListItems = new List<SelectListItem>();
            AvailableCollectionSelectListItems = new List<SelectListItem>();
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "LocalizedTitle")]
        public string LocalizedTitle { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Duration")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Sinopse")]
        public string About { get; set; }

        [Display(Name = "Argument")]
        public string Argument { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage.")]
        [Display(Name = "PosterImage")]
        public Microsoft.AspNetCore.Http.IFormFile PosterImageFile { get; set; }

        public System.Guid PosterImageId { get; set; }

        [Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage.")]
        [Display(Name = "PlacardImage")]
        public Microsoft.AspNetCore.Http.IFormFile PlacardImageFile { get; set; }

        public System.Guid PlacardImageId { get; set; }

        public bool ResetPlacardImage { get; set; }

        [Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "video/mp4", "video/flv" }, ErrorMessage = "RTMP_VIDEO_FileAllowedOnlyMessage.")]
        [Display(Name = "TrailerVideo")]
        public Microsoft.AspNetCore.Http.IFormFile TrailerVideoFile { get; set; }

        public System.Guid TrailerVideoId { get; set; }

        public string TrailerVideoExtension { get; set; }

        public bool ResetTrailerVideo { get; set; }

        [Display(Name = "Collection")]
        public System.Guid CollectionId { get; set; }

        public List<SelectListItem> AvailableCollectionSelectListItems { get; set; }

        [Display(Name = "Category")]
        public System.Guid CategoryId { get; set; }

        public List<SelectListItem> AvailableCategorySelectListItems { get; set; }

        [Display(Name = "Genres")]
        public List<SelectionItemInputViewModel<int>> SelectableGenres { get; set; }

        [Display(Name = "ProducerHouses")]
        public List<SelectionItemInputViewModel<long>> SelectableProducerHouses { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Rate")]
        [Range(1, 5)]
        public int Rate { get; set; }

        [Display(Name = "Clasification")]
        public int ClasificationId { get; set; }

        public IEnumerable<SelectListItem> Clasifications { get; set; }

        public Production Export()
        {
            var entity = new Production();

            Mergue(entity);

            return entity;
        }

        public void Import(Production entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            LocalizedTitle = entity.LocalizedTitle;
            Duration = entity.Duration;
            Year = entity.Year;
            About = entity.About;
            Argument = entity.Argument;
            LanguageId = entity.Language.Id;
            CountryId = entity.Country.Id;
            Rate = entity.Rate;
            ClasificationId = entity.Clasification.Id;
            PosterImageId = entity.PosterImage != null ? entity.PosterImage.Id : System.Guid.Empty;
            PlacardImageId = entity.PlacardImage?.Id ?? System.Guid.Empty;
            TrailerVideoId = entity.TrailerVideo != null ? entity.TrailerVideo.Id : System.Guid.Empty;
            TrailerVideoExtension = entity.TrailerVideo != null ? entity.TrailerVideo.Extension : string.Empty;
            CollectionId = entity.CollectionId ?? System.Guid.Empty;
            CategoryId = entity.CategoryId ?? System.Guid.Empty;
        }

        public void Mergue(Production entity)
        {
            entity.Title = Title;
            entity.LocalizedTitle = LocalizedTitle;
            entity.Duration = Duration;
            entity.Year = Year;
            entity.About = About;
            entity.Argument = Argument;
            entity.Rate = Rate;

            if (entity.CategoryId == null)
            {
                if (CategoryId != System.Guid.Empty)
                {
                    entity.CategoryId = CategoryId;
                }
            }
            else
            {
                if (CategoryId != System.Guid.Empty)
                {
                    if (entity.CategoryId.Value != CategoryId)
                    {
                        entity.CategoryId = CategoryId;
                    }
                }
                else
                {
                    entity.CategoryId = null;
                }
            }

            if (entity.CollectionId == null)
            {
                if (CollectionId != System.Guid.Empty)
                {
                    entity.CollectionId = CollectionId;
                }
            }
            else
            {
                if (CollectionId != System.Guid.Empty)
                {
                    if (entity.CollectionId.Value != CollectionId)
                    {
                        entity.CollectionId = CollectionId;
                    }
                }
                else
                {
                    entity.CollectionId = null;
                }
            }
        }
    }
}
