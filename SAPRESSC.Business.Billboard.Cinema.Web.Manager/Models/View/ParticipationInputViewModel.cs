using Microsoft.AspNetCore.Mvc.Rendering;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class ParticipationInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Participation, long>
    {

        public long Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Role")]
        public ProductionRole ProductionRole { get; set; }

        public IEnumerable<SelectListItem> ProductionRoles { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Personality")]
        public long PersonalityId { get; set; }

        public IEnumerable<SelectListItem> Personalities { get; set; }

        public long ProductionId { get; set; }

        [Display(Name = "Specification")]
        public string Specification { get; set; }

        public Participation Export()
        {
            var entity = new Participation();

            Mergue(entity);

            return entity;
        }

        public void Import(Participation entity)
        {
            Id = entity.Id;
            ProductionRole = entity.Role;
            PersonalityId = entity.Personality.Id;
            ProductionId = entity.Production.Id;
            Specification = entity.Specification;
        }

        public void Mergue(Participation entity)
        {
            entity.Role = ProductionRole;
            entity.Specification = Specification;
        }
    }
}
