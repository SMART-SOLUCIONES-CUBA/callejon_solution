namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class PersonalityCommentDelationDisplayViewModel : Core.Web.Management.Models.View.IEntityDisplayViewModel<Persistence.Entities.PersonalityCommentDelation, System.Guid>
    {
        public PersonalityCommentDelationDisplayViewModel()
        {
            Client = new ClientViewDisplayModel();
        }

        public System.Guid Id { get; set; }

        public System.DateTime Created { get; set; }

        public string Note { get; set; }

        public ClientViewDisplayModel Client { get; set; }

        public System.Guid CommentId { get; set; }

        public void Import(Persistence.Entities.PersonalityCommentDelation entity)
        {
            Id = entity.Id;
            Created = entity.Created;
            Note = entity.Note;
            CommentId = entity.Comment.Id;

            Client.Import(entity.Client);
        }
    }
}
