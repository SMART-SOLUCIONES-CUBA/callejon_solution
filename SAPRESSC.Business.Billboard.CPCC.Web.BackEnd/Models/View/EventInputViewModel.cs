using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class EventInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Event, Guid>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Cabecera")]
        public string Header { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        public bool Enabled { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Fecha y Hora de Inicio")]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Fecha y Hora de Fin")]
        public DateTime End { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Lugar")]
        public string Place { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Temática")]
        public string Thematic { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Organizador")]
        public string Organizer { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Acerca de")]
        public string About { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage.")]
        [Display(Name = "Image del Poster")]
        public Microsoft.AspNetCore.Http.IFormFile PosterImageFile { get; set; }

        public Guid PosterImageId { get; set; }
        [Core.Web.Common.ComponentModel.DataAnnotations.FormFileContentTypes(ContentTypes = new string[] { "video/mp4", "video/flv" }, ErrorMessage = "RTMP_VIDEO_FileAllowedOnlyMessage.")]
        [Display(Name = "Video Promocional")]
        public Microsoft.AspNetCore.Http.IFormFile PromoVideoFile { get; set; }

        public Guid PromoVideoId { get; set; }

        public string PromoVideoExtension { get; set; }

        public bool ResetPromoVideo { get; set; }

        public EventInputViewModel()
        {
            Start = DateTime.Now;
            End = DateTime.Now;
            Header = "Evento";
        }

        public Event Export()
        {
            var entity = new Event();

            Mergue(entity);

            return entity;
        }

        public void Import(Event entity)
        {
            Id = entity.Id;
            Header = entity.Header;
            Title = entity.Title;
            Enabled = entity.Enabled;
            Start = entity.Start;
            End = entity.End;
            Place = entity.Place;
            Thematic = entity.Thematic;
            About = entity.About;
            Description = entity.Description;
            Organizer = entity.Organizer;
            PosterImageId = entity.PosterImage.Id;
            PromoVideoId = entity.PromoVideo != null ? entity.PromoVideo.Id : Guid.Empty;
            PromoVideoExtension = entity.PromoVideo != null ? entity.PromoVideo.Extension : string.Empty;
        }

        public void Mergue(Event entity)
        {
            entity.Id = Id;
            entity.Header = Header;
            entity.Title = Title;
            entity.Enabled = Enabled;
            entity.Start = Start;
            entity.End = End;
            entity.Organizer = Organizer;
            entity.Place = Place;
            entity.Thematic = Thematic;
            entity.About = About;
            entity.Description = Description;
        }
    }
}
