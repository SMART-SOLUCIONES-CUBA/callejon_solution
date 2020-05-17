using Microsoft.AspNetCore.Mvc.Rendering;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class PresentationInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Presentation, long>
    {
        public const string PRODUCTIONID_QUERY_PARAM = "production";

        public PresentationInputViewModel()
        {
            PresentationDate = DateTime.Now.AddDays(1);
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "DateTime")]
        public DateTime PresentationDate { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Film")]
        public long ProductionId { get; set; }

        public Production Production { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Room")]
        public Guid RoomId { get; set; }

        public ICollection<SelectListItem> Rooms { get; set; }

        public bool Enabled { get; set; }

        public Presentation Export()
        {
            var entity = new Presentation();

            Mergue(entity);

            return entity;
        }

        public void Import(Presentation entity)
        {
            Id = entity.Id;
            PresentationDate = entity.Date;
            ProductionId = entity.Production.Id;
            Production = entity.Production;
            RoomId = entity.Room.Id;
            Enabled = entity.Enabled;
        }

        public void Mergue(Presentation entity)
        {
            entity.Date = PresentationDate;
            entity.Enabled = Enabled;
        }
    }
}
