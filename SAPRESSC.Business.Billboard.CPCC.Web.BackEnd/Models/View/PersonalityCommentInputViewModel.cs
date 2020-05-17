namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class PersonalityCommentInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<Persistence.Entities.PersonalityComment, System.Guid>
    {
        public System.Guid Id { get; set; }

        public Persistence.Entities.PersonalityComment Export()
        {
            throw new System.NotImplementedException();
        }

        public void Import(Persistence.Entities.PersonalityComment entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(Persistence.Entities.PersonalityComment entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
