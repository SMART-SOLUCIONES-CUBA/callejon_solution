namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class PersonalityCommentDisplayViewModel : AbstractCommentDisplayViewModel<Persistence.Entities.PersonalityComment>
    {
        public PersonalityCommentDisplayViewModel()
        {
            Personality = new Cinema.Web.Manager.Models.View.PersonalityDisplayViewModel();
        }

        public Cinema.Web.Manager.Models.View.PersonalityDisplayViewModel Personality { get; set; }

        public PersonalityCommentDisplayViewModel Reference { get; set; }

        public override void Import(Persistence.Entities.PersonalityComment entity)
        {
            base.Import(entity);

            DelationCount = entity.Delations.Count;

            Personality.Import(entity.Personality);

            if (entity.Reference != null)
            {
                Reference = new PersonalityCommentDisplayViewModel();

                Reference.Import(entity.Reference);
            }
        }
    }
}
