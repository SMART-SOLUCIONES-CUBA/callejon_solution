using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class AdviseInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Advise, int>
    {
        public int Id { get; set; }

        public bool Enabled { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Mensaje")]
        public string Message { get; set; }

        public Advise Export()
        {
            var entity = new Advise();

            Mergue(entity);

            return entity;
        }

        public void Import(Advise entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Message = entity.Message;
        }

        public void Mergue(Advise entity)
        {
            entity.Enabled = Enabled;
            entity.Message = Message;
        }
    }
}
