namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class AbstractCommentDisplayViewModel<TComment> : Core.Web.Management.Models.View.IEntityDisplayViewModel<TComment, System.Guid>
        where TComment : Persistence.Entities.Comment
    {
        public AbstractCommentDisplayViewModel()
        {
            Client = new ClientViewDisplayModel();
        }

        public System.Guid Id { get; set; }

        public string Message { get; set; }

        public System.DateTime Created { get; set; }

        public bool Banish { get; set; }

        public ClientViewDisplayModel Client { get; set; }

        public int DelationCount { get; set; }

        public virtual void Import(TComment entity)
        {
            Id = entity.Id;
            Message = entity.Message;
            Created = entity.Created;
            Banish = entity.Banish;

            Client.Import(entity.Client);
        }
    }
}
