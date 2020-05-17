namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class EventCommentInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Persistence.Entities.EventComment, System.Guid>
    {
        public System.Guid Id { get; set; }

        public Persistence.Entities.EventComment Export()
        {
            throw new System.NotImplementedException();
        }

        public void Import(Persistence.Entities.EventComment entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(Persistence.Entities.EventComment entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
