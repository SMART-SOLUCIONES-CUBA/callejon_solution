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

        [Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "video/mp4", "video/flv" }, ErrorMessage = "RTMP_VIDEO_FileAllowedOnlyMessage.")]
        [Display(Name = "TrailerVideo")]
        public Microsoft.AspNetCore.Http.IFormFile TrailerVideoFile { get; set; }

        public System.Guid TrailerVideoId { get; set; }

        public string TrailerVideoExtension { get; set; }

        public bool ResetTrailerVideo { get; set; }

        [Display(Name = "Genres")]
        public List<SelectionItemInputViewModel<int>> SelectableGenres { get; set; }

        [Display(Name = "ProducerHouses")]
        public List<SelectionItemInputViewModel<long>> SelectableProducerHouses { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Rate")]
        [Range(1, 5)]
        public int Rate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

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
            CategoryId = entity.Category.Id;
            PosterImageId = entity.PosterImage != null ? entity.PosterImage.Id : System.Guid.Empty;
            TrailerVideoId = entity.TrailerVideo != null ? entity.TrailerVideo.Id : System.Guid.Empty;
            TrailerVideoExtension = entity.TrailerVideo != null ? entity.TrailerVideo.Extension : string.Empty;
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
        }
    }
}
