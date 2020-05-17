namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class PersonalityCommentEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.PersonalityComment>
    {
        protected override string GetTableName()
        {
            return "personalitycomments";
        }
    }
}
