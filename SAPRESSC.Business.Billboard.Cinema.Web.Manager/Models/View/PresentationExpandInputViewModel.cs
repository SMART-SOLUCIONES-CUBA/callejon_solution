using System;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class PresentationExpandInputViewModel
    {
        [Required]
        public long PresentationId { get; set; }

        [Required]
        public DateTime PresentationStartDate { get; set; }

        [Required]
        public DateTime PresentationEndDate { get; set; }

        [Required]
        public DateTime PresentationTime { get; set; }

        public bool OverrideColision { get; set; }

        public string ProductionName { get; set; }

        public string RoomName { get; set; }

        public string CinemaName { get; set; }
    }
}
