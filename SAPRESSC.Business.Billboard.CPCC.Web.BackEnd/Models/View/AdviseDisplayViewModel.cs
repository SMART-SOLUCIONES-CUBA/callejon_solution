namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class AdviseDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Billboard.CPCC.Persistence.Entities.Advise, int>
    {
        public int Id { get; set; }

        public bool Enabled { get; set; }

        public string Message { get; set; }

        public void Import(Billboard.CPCC.Persistence.Entities.Advise entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Message = entity.Message;
        }
    }
}
