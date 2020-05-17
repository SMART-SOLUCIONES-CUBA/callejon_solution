using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class PresentationDuplicateInputViewModel
    {
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Presentation")]
        public long PresentationId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "DateTime")]
        public System.DateTime PresentationDate { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Room")]
        public System.Guid RoomId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Rooms { get; set; }

        public string ProductionName { get; set; }

        public string RoomName { get; set; }

        public string CinemaName { get; set; }
    }
}
