namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class EventCommentDisplayViewModel : AbstractCommentDisplayViewModel<Persistence.Entities.EventComment>
    {
        public EventCommentDisplayViewModel()
        {
            Event = new EventDisplayViewModel();
        }

        public EventDisplayViewModel Event { get; set; }

        public EventCommentDisplayViewModel Reference { get; set; }

        public override void Import(Persistence.Entities.EventComment entity)
        {
            base.Import(entity);

            DelationCount = entity.Delations.Count;

            Event.Import(entity.Event);

            if (entity.Reference != null)
            {
                Reference = new EventCommentDisplayViewModel();

                Reference.Import(entity.Reference);
            }
        }
    }
}
