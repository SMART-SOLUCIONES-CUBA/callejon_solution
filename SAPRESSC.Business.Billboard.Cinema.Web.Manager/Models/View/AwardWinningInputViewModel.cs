using Microsoft.AspNetCore.Mvc.Rendering;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.View
{
    public class AwardWinningInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<AwardWinning, int>
    {
        public AwardWinningInputViewModel()
        {
            WinTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = AwardWinType.Nomination.ToString(), Text = AwardWinType.Nomination.ToString() },
                new SelectListItem { Value = AwardWinType.Winner.ToString(), Text = AwardWinType.Winner.ToString() }
            };
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Film")]
        public long ProductionId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Edition")]
        public string Edition { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "WinType")]
        public AwardWinType WinType { get; set; }

        public IEnumerable<SelectListItem> WinTypes { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public int AwardId { get; set; }

        public IEnumerable<SelectListItem> Awards { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        public IEnumerable<SelectListItem> ProductionRoles { get; set; }

        public AwardWinning Export()
        {
            var entity = new AwardWinning();

            Mergue(entity);

            return entity;
        }

        public void Import(AwardWinning entity)
        {
            Id = entity.Id;
            Edition = entity.Edition;
            WinType = entity.Type;
            Category = entity.Category;
            AwardId = entity.Award.Id;
        }

        public void Mergue(AwardWinning entity)
        {
            entity.Edition = Edition;
            entity.Type = WinType;
            entity.Category = Category;
        }
    }
}
