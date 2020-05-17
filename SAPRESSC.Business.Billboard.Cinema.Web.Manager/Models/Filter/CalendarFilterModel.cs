using System;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.Filter
{
    public class CalendarFilterModel
    {
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "FirstDate")]
        public DateTime FirstDate { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "LastDate")]
        public DateTime LastDate { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Room")]
        public Guid RoomId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Rooms { get; set; }
    }
}
