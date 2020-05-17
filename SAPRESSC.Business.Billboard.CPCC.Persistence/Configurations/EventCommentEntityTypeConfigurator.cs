namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Configurations
{
    public class EventCommentEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.EventComment>
    {
        protected override string GetTableName()
        {
            return "eventcomments";
        }
    }
}
